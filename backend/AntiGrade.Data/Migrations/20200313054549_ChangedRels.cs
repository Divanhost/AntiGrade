using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class ChangedRels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentWorks");

            migrationBuilder.CreateTable(
                name: "StudentCriterias",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    TotalPoints = table.Column<decimal>(type: "decimal(18,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCriterias", x => new { x.StudentId, x.CriteriaId });
                    table.ForeignKey(
                        name: "FK_StudentCriterias_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCriterias_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_StudentCriterias_CriteriaId",
                table: "StudentCriterias",
                column: "CriteriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCriterias");

            migrationBuilder.CreateTable(
                name: "StudentWorks",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    WorkId = table.Column<int>(nullable: false),
                    TotalPoints = table.Column<decimal>(type: "decimal(18,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWorks", x => new { x.StudentId, x.WorkId });
                    table.ForeignKey(
                        name: "FK_StudentWorks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentWorks_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ed9f0398-cb00-4492-ae62-214494b3c175");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dea8b4f9-1787-4ffd-b1ea-3526b524f05b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8f814709-8ce5-44b0-8a98-adcf41fa9845");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "3607c95c-86d1-46d0-8360-a46104c9126e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "28da92ee-a13b-4892-af18-d68b9f950b61");

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorks_WorkId",
                table: "StudentWorks",
                column: "WorkId");
        }
    }
}
