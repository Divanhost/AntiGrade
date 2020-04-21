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
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<StudentCriteria> StudentCriterias { get; set; }
        public DbSet<StudentWork> StudentWorks { get; set; }
        public DbSet<SubjectEmployee> SubjectEmployees { get; set; }
        public DbSet<ExamResult> ExamResult { get; set; }
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

            builder.Entity<StudentCriteria>()
                .HasKey(sw => new { sw.StudentId, sw.CriteriaId, sw.IsAdditional });
            builder.Entity<StudentCriteria>()
                .HasOne(sw => sw.Student)
                .WithMany(s => s.StudentCriterias)
                .HasForeignKey(sw => sw.StudentId);
            builder.Entity<StudentCriteria>()
                .HasOne(sw => sw.Criteria)
                .WithMany(w => w.StudentCriterias)
                .HasForeignKey(sw => sw.CriteriaId);

            builder.Entity<StudentWork>()
                .HasKey(sw => new { sw.StudentId, sw.WorkId, sw.IsAdditional });
            builder.Entity<StudentWork>()
                .HasOne(sw => sw.Student)
                .WithMany(s => s.StudentWorks)
                .HasForeignKey(sw => sw.StudentId);
            builder.Entity<StudentWork>()
                .HasOne(sw => sw.Work)
                .WithMany(w => w.StudentWorks)
                .HasForeignKey(sw => sw.WorkId);
            
            builder.Entity<SubjectEmployee>()
                .HasKey(sw => new { sw.SubjectId, sw.EmployeeId });
            builder.Entity<SubjectEmployee>()
                .HasOne(sw => sw.Subject)
                .WithMany(s => s.SubjectEmployees)
                .HasForeignKey(sw => sw.SubjectId);
            builder.Entity<SubjectEmployee>()
                .HasOne(sw => sw.Employee)
                .WithMany(w => w.SubjectEmployees)
                .HasForeignKey(sw => sw.EmployeeId);
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
