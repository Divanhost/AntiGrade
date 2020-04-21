using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class addedAdditionalFlagKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCriterias",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsAdditional = table.Column<bool>(nullable: false),
                    TotalPoints = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Touched = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCriterias", x => new { x.StudentId, x.CriteriaId });
                    table.UniqueConstraint("AK_StudentCriterias_CriteriaId_IsAdditional_StudentId", x => new { x.CriteriaId, x.IsAdditional, x.StudentId });
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
                    Id = table.Column<int>(nullable: false),
                    IsAdditional = table.Column<bool>(nullable: false),
                    SumOfPoints = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Touched = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWorks", x => new { x.StudentId, x.WorkId });
                    table.UniqueConstraint("AK_StudentWorks_IsAdditional_StudentId_WorkId", x => new { x.IsAdditional, x.StudentId, x.WorkId });
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
                value: "7e45f419-1569-4c0c-9071-4e4dfc44042f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "eec3696e-fe9b-40f8-a019-f33d6d299d02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a6127079-d26b-4fcf-aa67-b2c6e0c60258");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "cdc59cea-dd2f-4a20-b55f-76a1f1e5fd69");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "7b78fe58-cd20-44d8-b6ba-46afb7b8363b");

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorks_WorkId",
                table: "StudentWorks",
                column: "WorkId");
        }
    }
}
