using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Shared;
using AntiGrade.Shared.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntiGrade.Web.Controllers.Configuration
{
    [Route("api/users")]
    [ApiController]
    //[Authorize]
    public class UserController : BaseServiceController<IUserService>
    {

        public UserController(IUserService userService) : base(userService)
        { }
        [AllowAnonymous]
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _service.GetAllRoles();
            return ResponseModel(result);
        }
        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _service.GetUserById(userId);
            return ResponseModel(result);
        }
        [AllowAnonymous]
        [HttpGet("check_username/{username}")]
        public async Task<IActionResult> CheckUserNameExists(string username)
        {
            var result = await _service.CheckUserNameExists(username);
            return ResponseModel(result);
        }
        [AllowAnonymous]
        [HttpGet("check_email/{email}")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var result = await _service.CheckEmailExists(email);
            return ResponseModel(result);
        }
        [AllowAnonymous]
        [HttpGet("check_password/{userName}/{password}")]
        public async Task<IActionResult> CheckPassword(string userName, string password)
        {
            var result = await _service.CheckPasswordAsync(userName, password);
            return ResponseModel(result);
        }

       // [Authorize(Roles = Roles.Admin)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            var result = await _service.CreateUser(user);
            return ResponseModel(result);
        }
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUserByID(int id, [FromBody] UserDto user)
        {
            var result = await _service.UpdateUserByID(id, user);
            return ResponseModel(result);
        }
       // [Authorize(Roles = Roles.Admin)]
        [AllowAnonymous]
        [HttpDelete("delete_user/{userId:int}")]

        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool result = await _service.DeleteById(userId);
            return ResponseModel(result);
        }
    }
}