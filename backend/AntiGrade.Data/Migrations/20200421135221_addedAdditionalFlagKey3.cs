using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class addedAdditionalFlagKey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCriterias",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    IsAdditional = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    TotalPoints = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Touched = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCriterias", x => new { x.StudentId, x.CriteriaId, x.IsAdditional });
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

            migrationBuilder.CreateTable(
                name: "StudentWorks",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    WorkId = table.Column<int>(nullable: false),
                    IsAdditional = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    SumOfPoints = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Touched = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWorks", x => new { x.StudentId, x.WorkId, x.IsAdditional });
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
                value: "4fa223c5-8c83-43d8-a0cf-99312e6c4f62");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d435c78c-9a31-4c3c-a1c7-5155295ff329");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "63ebf576-a18d-413b-b5c8-5e702f5b4535");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "970ed43b-b3f4-4aba-add6-c0efddbe451f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "03a8fb79-b99c-4f75-8270-bfda6f031e6d");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCriterias_CriteriaId",
                table: "StudentCriterias",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorks_WorkId",
                table: "StudentWorks",
                column: "WorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCriterias");

            migrationBuilder.DropTable(
                name: "StudentWorks");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "24750040-7cdc-4a60-b345-602e9ee24876");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bfb4a82d-af53-4e51-a69b-c2f1d033549f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "53800d1f-8c9e-4abd-9ffe-54ed9083c9d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "c11b1e34-cb24-417d-beca-1d1a48b77a60");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "a5ba40c3-28dd-4989-91c5-5879e18d960b");
        }
    }
}
