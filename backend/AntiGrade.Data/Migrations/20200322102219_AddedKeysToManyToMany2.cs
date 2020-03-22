using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class AddedKeysToManyToMany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentWorks_Id",
                table: "StudentWorks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentCriterias_Id",
                table: "StudentCriterias");

            migrationBuilder.AddColumn<bool>(
                name: "Touched",
                table: "StudentWorks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Touched",
                table: "StudentCriterias",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ab398918-184f-476d-9dd0-9cf591cbd7a1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1214bb84-05cd-44a4-85d5-4d625348edf9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5baa2eb3-5275-4ba5-928b-48d81bfba283");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "6431044a-4197-4327-8696-23317a9ffd74");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "ef3cff7b-354c-45e6-9a8c-4ffdfb1ec21e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Touched",
                table: "StudentWorks");

            migrationBuilder.DropColumn(
                name: "Touched",
                table: "StudentCriterias");

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
    }
}
