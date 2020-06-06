using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class RemovedEPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Semesters_SemestrId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "EmployeePositions");

            migrationBuilder.DropIndex(
                name: "IX_ExamResult_StudentId",
                table: "ExamResult");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeePositionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeePositionId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "SemestrId",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsExamClosed",
                table: "Subjects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExamStarted",
                table: "Subjects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstRetakeClosed",
                table: "Subjects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSecondRetakeClosed",
                table: "Subjects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b9d63bd8-8042-437f-8eab-af2856869785");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "16641311-4733-464e-9229-c451336df659");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "bf7edcc2-a652-4609-a244-ff2bc390cf61");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResult_StudentId",
                table: "ExamResult",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Semesters_SemestrId",
                table: "Subjects",
                column: "SemestrId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Semesters_SemestrId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_ExamResult_StudentId",
                table: "ExamResult");

            migrationBuilder.DropColumn(
                name: "IsExamClosed",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsExamStarted",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsFirstRetakeClosed",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsSecondRetakeClosed",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "SemestrId",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeePositionId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeePositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "685774a5-98ce-4bf2-ad3d-92ef1e541950");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ec2fd6c7-fd33-4b17-81e3-ae8bd760bf7c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "5527c673-96f3-45af-8270-4b61c7522cf6");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResult_StudentId",
                table: "ExamResult",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Semesters_SemestrId",
                table: "Subjects",
                column: "SemestrId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
