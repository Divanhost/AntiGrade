using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class addedAdditionalFlagKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentCriterias_CriteriaId",
                table: "StudentCriterias");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentWorks_IsAdditional_StudentId_WorkId",
                table: "StudentWorks",
                columns: new[] { "IsAdditional", "StudentId", "WorkId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentCriterias_CriteriaId_IsAdditional_StudentId",
                table: "StudentCriterias",
                columns: new[] { "CriteriaId", "IsAdditional", "StudentId" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentWorks_IsAdditional_StudentId_WorkId",
                table: "StudentWorks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentCriterias_CriteriaId_IsAdditional_StudentId",
                table: "StudentCriterias");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dde0dda4-73b9-4d57-a1e3-25ffa9fd354c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d7bf33bc-da20-4081-87ec-a227036f2c21");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e89d3b7f-d4a0-47f2-b947-d27a1e06d0a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "89087ab9-7cfa-4712-b1ee-23489f312230");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "13738d50-7a8e-4bb3-aa4d-79bde03e00d7");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCriterias_CriteriaId",
                table: "StudentCriterias",
                column: "CriteriaId");
        }
    }
}
