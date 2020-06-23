using System;
using System.Collections.Generic;
using AntiGrade.Shared.InputModels;

namespace AntiGrade.Tests
{
    public static class TestDtoObjects
    {
        public const int CustomerId = 1;
        public static DateTime UtcNow
        {
            get;
        } = DateTime.UtcNow;

       

        public static UserDto CorrectUserDto => new UserDto
        {
            UserName = "NewTestUser",
            Email = "user2@user.com",
            NewPassword = "Test123test!",
            OldPassword = "Test123test!",
            Roles = new string[]
            {
            "User"
            }
        };
        public static UserDto DuplicateUserDto => new UserDto
        {
            UserName = "DuplicateUser",
            Email = "userDP@user.com",
            NewPassword = "Test123test!",
            OldPassword = "Test123test!",
            Roles = new string[]
            {
            "User"
            },
        };
    }
}
