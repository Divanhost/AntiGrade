using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class ChangedSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ExamType_TypeId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Employees",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "38efda9c-00ff-49ae-8740-9407d20eddad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b178274a-cf7d-4aba-9b69-f8a7981d7241");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ea223e5e-2d56-47f2-9c1d-58d9a9f085f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "6c55d094-a29d-4c14-87af-fba7a6c3bcaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "5e01774f-f04f-4c72-9c0a-7cdb5a8187cc");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ExamType_TypeId",
                table: "Subjects",
                column: "TypeId",
                principalTable: "ExamType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Subjects_SubjectId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ExamType_TypeId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SubjectId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ExamType_TypeId",
                table: "Subjects",
                column: "TypeId",
                principalTable: "ExamType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
