using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyApp.Dal.EfStructures.Migrations
{
    public partial class TimestampAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Orders",
                type: "timestamp",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Employees",
                type: "timestamp",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Departments",
                type: "timestamp",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "TimeStamp",
                table: "Orders",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldRowVersion: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<byte[]>(
                name: "TimeStamp",
                table: "Employees",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldRowVersion: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<byte[]>(
                name: "TimeStamp",
                table: "Departments",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldRowVersion: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }
    }
}
