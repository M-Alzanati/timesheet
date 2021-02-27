using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeSheetAPI.Migrations.TimeSheetDb
{
    public partial class ChangeSubmissionSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Logout",
                table: "SubmissionSheets",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Login",
                table: "SubmissionSheets",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Logout",
                table: "SubmissionSheets",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Login",
                table: "SubmissionSheets",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
