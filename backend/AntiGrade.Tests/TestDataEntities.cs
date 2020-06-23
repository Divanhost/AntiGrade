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
        public const int ProjectNotificationsUserId = 4;
        public const int DeveloperEmployeeId = 1;
        public const int FiredDeveloperEmployeeId = 2;
        public const int SalesEmployeeId = 3;
        public const int ProjectNotificationsEmployeeId = 4;
        public const int NonPrivilegedUserId = 2;
        public const int DepartmentId = 1;
        public const int AdminAccountId = 1;
        public const int NonPrivilegedUserAccountId = 2;
        public const int NonPrivilegedUserAccount2Id = 3;
        public const int NonPrivilegedUserAccount3Id = 4;
        public const int HelperAccountId = 5;
        public const int DeletedAccountUserHasAccessId = 6;
        public const int NotificationsAccountId = 7;
        public const int RubCurrencyId = 1;
        public const int NotificationsProjectId = 1;

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

        public static User ProjectNotificationsUser => new User
        {
            Id = ProjectNotificationsUserId,
            UserName = "ProjectNotificationsUser",

            Roles = new List<UserRole>
            {
                GetAdminRole(ProjectNotificationsUserId)
            }
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
