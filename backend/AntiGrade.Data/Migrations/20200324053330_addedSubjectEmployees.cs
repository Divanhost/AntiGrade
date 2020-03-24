using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class addedSubjectEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Subjects_SubjectId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SubjectId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "SubjectEmployees",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectEmployees", x => new { x.SubjectId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_SubjectEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectEmployees_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2c9a067d-2ecf-48e3-ab15-f7e0b0126097");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "922e1644-0f12-4452-98ed-861a340f7238");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bc62b98a-8a5c-4cb0-99d5-e586d4a3fdad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "99eb8dbe-fe73-4193-91b7-2ca2bb420c00");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "1221b34b-dbe8-4c08-b9a6-efb3844b53cf");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectEmployees_EmployeeId",
                table: "SubjectEmployees",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectEmployees");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Employees",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d0e0285b-c327-43c1-b202-2ab3e2bbcbe6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2d7158d2-a8c7-47b9-a531-5936374a319d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5e0e9724-3129-4e82-9c84-1b0320050af8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "f7d596d6-ef9b-49a9-a835-118879acbee8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "5709f279-84aa-4dc7-9fb9-a59356c7acf6");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SubjectId",
                table: "Employees",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Subjects_SubjectId",
                table: "Employees",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
