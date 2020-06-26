using System;
using System.Collections.Generic;
using System.Linq;
using AntiGrade.Shared.Models.Identity;

namespace AntiGrade.Tests
{
    public static class TestDataEntities
    {

        public const int AdminRoleId = 1;
        public const int AdminUserId = 1;
        public const int NewUserId = 2;
        public const int DeletedUserId = 3;
        public const int NonPrivilegedUserId = 4;

        public static DateTime UtcNow
        {
            get;
        } = DateTime.UtcNow;

        public static User AdminUser => new User
        {
            Id = AdminUserId,
            UserName = "admin",
            SecurityStamp = "",
            Roles = new List<UserRole>
            {
            GetAdminRole(AdminUserId)
            }
        };

        public static User NonPrivilegedUser => new User
        {
            Id = NonPrivilegedUserId,
            UserName = "simpleUser",
            Roles = new List<UserRole> { }
        };

        public static User NewUser => new User
        {
            Id = NewUserId,
            UserName = "NewUser",
            Roles = new List<UserRole> { }
        };

        public static User DeletedUser => new User
        {
            Id = DeletedUserId,
            UserName = "DeletedUser",
            IsDeleted = true,
            Roles = new List<UserRole> { }
        };

        public static UserRole GetAdminRole(int userId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = AdminRoleId
            };
            return userRole;
        }

    }
}
