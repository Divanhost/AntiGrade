using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class AddedExamTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Subjects",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExamType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f2f29362-1ce6-4ad3-ae86-4d6031b7c954");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e7b8c37b-5b6f-4552-b4a6-5b4b2863fa36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5b5a82b4-c89c-41e6-a5b5-c1b37b6375d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "1447352d-6802-49a8-8f83-48069532e782");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "7ba5562d-6c57-44fb-a04d-a646a798dfa4");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TypeId",
                table: "Subjects",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ExamType_TypeId",
                table: "Subjects",
                column: "TypeId",
                principalTable: "ExamType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ExamType_TypeId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "ExamType");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TypeId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ad112226-e1ac-4cf4-b133-f97a5da817d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4642397e-dae2-487a-b5ba-8263bc70446b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "501ae7fe-84f6-4dd7-a483-69ccc287aba1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "348acf61-ede1-4dc4-891d-046f32ad7880");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "fedcb80a-3d5e-45ab-8f57-30c298e69018");
        }
    }
}
