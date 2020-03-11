using System;
using AntiGrade.Shared.Models;
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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<TokenCouple> TokenCouples { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectDistribution> SubjectDistribution { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<StudentWork> StudentWorks { get; set; }
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

            builder.Entity<StudentWork>()
                .HasKey(sw => new { sw.StudentId, sw.WorkId });
            builder.Entity<StudentWork>()
                .HasOne(sw => sw.Student)
                .WithMany(s => s.StudentWorks)
                .HasForeignKey(sw => sw.StudentId);
            builder.Entity<StudentWork>()
                .HasOne(sw => sw.Work)
                .WithMany(w => w.StudentWorks)
                .HasForeignKey(sw => sw.WorkId);
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
