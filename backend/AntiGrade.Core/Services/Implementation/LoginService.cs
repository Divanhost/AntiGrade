using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Data.Repositories.Interfaces;
using AntiGrade.Shared;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using BusinessIntelligence.BusinessObjects.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntiGrade.Core.Services.Implementation
{
    public class TokenService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        public TokenService(IUnitOfWork unitOfWork,
            IConfigurationService configuration,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> LoginAsync(LoginDto user)
        {
            var appUser = await _unitOfWork.UserRepository.Filter(x => x.UserName == user.UserName && !x.IsDeleted)
            .SingleOrDefaultAsync();

            if (appUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(appUser, user.Password);

                if (result)
                {
                    return await GenerateJwtToken(user.UserName, appUser);
                }
            }

            throw new WebsiteException("Login Failed! Incorrect login or password!");
        }

        public async Task<TokenCouple> RenewAsync(TokenCouple tokenCouple)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtObject = handler.ReadJwtToken(tokenCouple.Jwt);
            var userId = jwtObject.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
            var tokenCoupleFromDb = await _unitOfWork.TokenCoupleRepository
                .Filter(x => x.Refresh == tokenCouple.Refresh && x.Jwt == tokenCouple.Jwt)
                .FirstOrDefaultAsync();
            if (tokenCoupleFromDb != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (!user.IsDeleted)
                {
                    return await GetTokenCouple(user.UserName, user);
                }
                else
                {
                    throw new WebsiteException("Renew Failed! User has been deleted!");
                }
            }
            else
            {
                throw new WebsiteException("Renew Failed! Refresh token doesn't exist!");
            }
        }

        public async Task<User[]> GetAllUsers()
        {
            var result = await _unitOfWork.UserRepository.All().ToArrayAsync();
            return result;
        }

        private async Task<TokenCouple> GetTokenCouple(string login, User appUser)
        {
            var result = new TokenCouple()
            {
                Jwt = await GenerateJwtToken(login, appUser),
                Refresh = GenerateRefreshToken()
            };

            var tokenCouple = _unitOfWork.TokenCoupleRepository.Create(result);
            await _unitOfWork.Save();

            return tokenCouple;
        }

        private string GenerateRefreshToken()
        {
            var random = new Random();
            byte[] bytes = new Byte[32];
            random.NextBytes(bytes);
            var token = Convert.ToBase64String(bytes);
            return token;
        }

        private async Task<string> GenerateJwtToken(string login, User appUser)
        {
            var userRoles = await _userManager.GetRolesAsync(appUser);

            var roleClaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                roleClaims.Add(
                    new Claim(CustomClaims.Roles, role)
                );
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, appUser.Id.ToString())
            };
            // if (appUser.Employee != null)
            // {
            //     claims.Add(new Claim(CustomClaims.EmployeeId, appUser.Employee.Id.ToString()));
            // }

            claims.AddRange(roleClaims);

            var jwtKey = Encoding.UTF8.GetBytes(_configuration.JwtKey);

            var key = new SymmetricSecurityKey(jwtKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var configExpireSeconds = _configuration.JwtExpireSeconds;
            var expires = DateTime.Now.AddSeconds(configExpireSeconds);

            var token = new JwtSecurityToken(
                _configuration.JwtIssuer,
                _configuration.JwtAudience,
                claims,
                expires: expires,
                signingCredentials: creds,
                notBefore: DateTime.Now.Subtract(TimeSpan.FromMinutes(5))
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigurationService _configuration;
    }
}