using System;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Implementation;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Shared.Exceptions;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnitTests;
using Xunit;

namespace BusinessIntelligence.Core.Tests.Common
{
    public class UserServiceTest : DatabaseBaseTest
    {
        protected IUserService Service
        {
            get;
            set;
        }

        public UserServiceTest()
        {
            Service = ServiceProvider.GetService<IUserService>();
        }

        [Fact]
        public async Task CreateUser_ValidObjectPassed_Saves()
        {
            var result = await Service.CreateUser(TestDtoObjects.CorrectUserDto);
            Assert.Equal(result.UserName, TestDtoObjects.CorrectUserDto.UserName);
            Assert.Equal(result.Email, TestDtoObjects.CorrectUserDto.Email);
            bool deleteResult = await Service.DeleteById(result.Id);
            Assert.True(deleteResult);
        }

        [Fact]
        public async Task CreateUser_EmptyObjectPassed_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => Service.CreateUser(new UserDto()));
        }

        [Fact]
        public async Task CreateUser_NullPassed_Throws()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => Service.CreateUser(null));
        }

        [Fact]
        public async Task CreateUser_ExistingObjectPassed_Throws()
        {
            await Service.CreateUser(TestDtoObjects.DuplicateUserDto);
            var exception = await Assert.ThrowsAsync<WebsiteException>(() => Service.CreateUser(TestDtoObjects.DuplicateUserDto));
            Assert.Equal(UserService.ExistingUserMessage, exception.Message);
        }

        protected override async Task SeedAsync()
        {
            await base.SeedAsync();
            TDbContext.Set<User>().Add(TestDataEntities.AdminUser);
            await TDbContext.SaveChangesAsync();
        }
    }
}
