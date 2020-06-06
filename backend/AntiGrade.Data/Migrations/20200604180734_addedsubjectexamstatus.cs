using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AntiGrade.Data.Migrations
{
    public partial class addedsubjectexamstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "SubjectExamStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubjectId = table.Column<int>(nullable: false),
                    IsExamStarted = table.Column<bool>(nullable: false),
                    IsExamClosed = table.Column<bool>(nullable: false),
                    IsFirstRetakeStarted = table.Column<bool>(nullable: false),
                    IsFirstRetakeClosed = table.Column<bool>(nullable: false),
                    IsSecondRetakeStarted = table.Column<bool>(nullable: false),
                    IsSecondRetakeClosed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectExamStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectExamStatuses_Subjects_SubjectId",
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
                value: "94bfae49-194e-4f50-a692-b64b2be4c4b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dfbba9d9-cdd2-430c-893d-3caf05c98203");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "9e619ad9-eb15-4792-bde1-d2bfb44b3706");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectExamStatuses_SubjectId",
                table: "SubjectExamStatuses",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectExamStatuses");

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
        }
    }
}
