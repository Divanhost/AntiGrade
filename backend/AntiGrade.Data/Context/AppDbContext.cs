using System;
using AntiGrade.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AntiGrade.Data.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<Role>().HasData(
                GetRole(1, AntiGrade.Shared.Roles.Admin),
                GetRole(2, AntiGrade.Shared.Roles.Teacher),
                GetRole(3, AntiGrade.Shared.Roles.Student),
                GetRole(4, AntiGrade.Shared.Roles.Lecturer),
                GetRole(6, AntiGrade.Shared.Roles.User)
            );
        }

        private Role GetRole(int id, string role)
        {
            var roleObj = new Role
            {
                Id = id,
                Name = role,
                NormalizedName = role.ToUpper()
            };
            return roleObj;
        }
    }
}
