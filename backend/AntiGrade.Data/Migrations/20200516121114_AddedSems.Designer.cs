﻿// <auto-generated />
using System;
using AntiGrade.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AntiGrade.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200516121114_AddedSems")]
    partial class AddedSems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AntiGrade.Shared.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Criteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("MaxPoints")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("WorkId");

                    b.HasKey("Id");

                    b.HasIndex("WorkId");

                    b.ToTable("Criterias");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InstituteId");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("EmployeePositionId");

                    b.Property<string>("FirstName")
                        .HasMaxLength(70);

                    b.Property<bool>("IsFired");

                    b.Property<string>("LastName")
                        .HasMaxLength(70);

                    b.Property<string>("Patronymic")
                        .HasMaxLength(70);

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeePositionId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.EmployeePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("EmployeePositions");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.ExamResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Points")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("SecondPassPoints")
                        .HasColumnType("decimal(18,5)");

                    b.Property<int>("StudentId");

                    b.Property<int>("SubjectId");

                    b.Property<decimal>("ThirdPassPoints")
                        .HasColumnType("decimal(18,5)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ExamResult");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.ExamType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ExamType");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "ae57cf05-4095-467d-a56b-1ed716758792",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "05d65c6c-e2be-4e69-8a8f-567128ca9e43",
                            Name = "Teacher",
                            NormalizedName = "TEACHER"
                        },
                        new
                        {
                            Id = 6,
                            ConcurrencyStamp = "ffa39c34-c70f-49a7-8e20-25d9370799c5",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Identity.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("RoleId1");

                    b.Property<int?>("UserId1");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Institute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Mode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("WorkMode");

                    b.HasKey("Id");

                    b.ToTable("Mode");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Year");

                    b.Property<bool>("isFirstHalf");

                    b.HasKey("Id");

                    b.ToTable("Semester");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ответственный преподаватель"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Лектор"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Преподаватель практики"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Преподаватель лабораторных занятий"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Экзаменатор"
                        });
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasMaxLength(70);

                    b.Property<int>("GroupId");

                    b.Property<string>("LastName")
                        .HasMaxLength(70);

                    b.Property<string>("Patronymic")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.StudentCriteria", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("CriteriaId");

                    b.Property<bool>("IsAdditional");

                    b.Property<int>("Id");

                    b.Property<decimal>("TotalPoints")
                        .HasColumnType("decimal(18,5)");

                    b.Property<bool>("Touched");

                    b.HasKey("StudentId", "CriteriaId", "IsAdditional");

                    b.HasIndex("CriteriaId");

                    b.ToTable("StudentCriterias");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.StudentWork", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("WorkId");

                    b.Property<bool>("IsAdditional");

                    b.Property<int>("Id");

                    b.Property<decimal>("SumOfPoints")
                        .HasColumnType("decimal(18,5)");

                    b.Property<bool>("Touched");

                    b.HasKey("StudentId", "WorkId", "IsAdditional");

                    b.HasIndex("WorkId");

                    b.ToTable("StudentWorks");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<bool>("HasBonuses");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("SemestrId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SemestrId");

                    b.HasIndex("TypeId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.SubjectEmployee", b =>
                {
                    b.Property<int>("SubjectId");

                    b.Property<int>("EmployeeId");

                    b.Property<int>("StatusId");

                    b.Property<int>("Id");

                    b.HasKey("SubjectId", "EmployeeId", "StatusId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StatusId");

                    b.ToTable("SubjectEmployees");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.TokenCouple", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Jwt");

                    b.Property<string>("Refresh");

                    b.HasKey("Id");

                    b.ToTable("TokenCouples");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("MaxPoints")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("SubjectId");

                    b.Property<int>("WorkTypeId");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.WorkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("WorkTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Criteria", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Work", "Work")
                        .WithMany("Criterias")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Department", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Institute", "Institute")
                        .WithMany("Departments")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Employee", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("AntiGrade.Shared.Models.EmployeePosition", "EmployeePosition")
                        .WithMany()
                        .HasForeignKey("EmployeePositionId");

                    b.HasOne("AntiGrade.Shared.Models.Identity.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("AntiGrade.Shared.Models.Employee", "UserId");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.ExamResult", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Group", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Course", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Identity.UserRole", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Identity.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Identity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId1");

                    b.HasOne("AntiGrade.Shared.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Identity.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Student", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.StudentCriteria", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Criteria", "Criteria")
                        .WithMany("StudentCriterias")
                        .HasForeignKey("CriteriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Student", "Student")
                        .WithMany("StudentCriterias")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.StudentWork", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Student", "Student")
                        .WithMany("StudentWorks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Work", "Work")
                        .WithMany("StudentWorks")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Subject", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Group", "Group")
                        .WithMany("Subjects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Semester", "Semester")
                        .WithMany("Subjects")
                        .HasForeignKey("SemestrId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.ExamType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.SubjectEmployee", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Employee", "Employee")
                        .WithMany("SubjectEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.Subject", "Subject")
                        .WithMany("SubjectEmployees")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AntiGrade.Shared.Models.Work", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Subject", "Subject")
                        .WithMany("Works")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AntiGrade.Shared.Models.WorkType", "WorkType")
                        .WithMany()
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Identity.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("AntiGrade.Shared.Models.Identity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}