using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class AddedKeysToManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentWorks_Id",
                table: "StudentWorks",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentCriterias_Id",
                table: "StudentCriterias",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "76dac87f-30ec-43d0-8a8b-7030177d9927");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7860ec1b-ae38-40cd-a36c-ba10d6ab4974");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a9220617-dd6f-4bde-a5ca-a19f9872fc79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "9badf03d-9214-45dd-9714-e150213d9493");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "b35d3247-150b-4638-8bad-3857342cbf0e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentWorks_Id",
                table: "StudentWorks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentCriterias_Id",
                table: "StudentCriterias");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "62691f30-6131-472d-be95-f1a878b1a566");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0ee6c3a7-aa9b-4290-9be7-e0179c68e23a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6a1c6ca5-15a6-4ef6-8b93-b0f31cad8d5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a0880f02-1a61-4e9a-bd58-84a6eb0e583a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "046d051d-6822-4d3f-a890-cffa2b7b40e8");
        }
    }
}
