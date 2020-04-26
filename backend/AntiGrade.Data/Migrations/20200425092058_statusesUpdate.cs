using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class statusesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectEmployees",
                table: "SubjectEmployees");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubjectEmployees");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "SubjectEmployees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectEmployees",
                table: "SubjectEmployees",
                columns: new[] { "SubjectId", "EmployeeId", "StatusId" });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7577748a-ecfe-4e42-841d-52e740bec109");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "653bbb10-4216-4e5d-a539-c68a9cced288");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6c2c7b9c-2526-432c-b09a-66c6d3b6eb21");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a9eda91c-bf9f-4f3d-8e06-d9d81b1dd001");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "fb98eda6-7bd5-4007-8594-32b27464993e");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ответственный преподаватель" },
                    { 2, "Лектор" },
                    { 3, "Преподаватель практики" },
                    { 4, "Преподаватель лабораторных занятий" },
                    { 5, "Экзаменатор" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectEmployees_StatusId",
                table: "SubjectEmployees",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectEmployees_Statuses_StatusId",
                table: "SubjectEmployees",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectEmployees_Statuses_StatusId",
                table: "SubjectEmployees");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectEmployees",
                table: "SubjectEmployees");

            migrationBuilder.DropIndex(
                name: "IX_SubjectEmployees_StatusId",
                table: "SubjectEmployees");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "SubjectEmployees");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SubjectEmployees",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectEmployees",
                table: "SubjectEmployees",
                columns: new[] { "SubjectId", "EmployeeId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bb2a63e3-8d74-45ed-833c-31755e735b0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "14937044-f2b1-472b-a33b-fcbb747f979c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "72519b90-aee1-44b7-933b-b3fac4ba0a53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "b9cc0d06-2ec3-4cdc-ad90-aeabeac9de5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "db015fc7-13e5-441d-bb9d-1eca452caec0");
        }
    }
}
