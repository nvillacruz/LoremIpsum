using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoremIpsum.Web.Server.Migrations
{
    public partial class AddEnterpriseSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateModified",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2020, 4, 4, 5, 1, 52, 862, DateTimeKind.Unspecified).AddTicks(7986), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2020, 2, 15, 4, 29, 13, 746, DateTimeKind.Unspecified).AddTicks(4681), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2020, 4, 4, 5, 1, 52, 856, DateTimeKind.Unspecified).AddTicks(3985), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2020, 2, 15, 4, 29, 13, 740, DateTimeKind.Unspecified).AddTicks(1937), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "EnterpriseSetting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseSetting", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterpriseSetting");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateModified",
                table: "Employee",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2020, 2, 15, 4, 29, 13, 746, DateTimeKind.Unspecified).AddTicks(4681), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValue: new DateTimeOffset(new DateTime(2020, 4, 4, 5, 1, 52, 862, DateTimeKind.Unspecified).AddTicks(7986), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "Employee",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2020, 2, 15, 4, 29, 13, 740, DateTimeKind.Unspecified).AddTicks(1937), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValue: new DateTimeOffset(new DateTime(2020, 4, 4, 5, 1, 52, 856, DateTimeKind.Unspecified).AddTicks(3985), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
