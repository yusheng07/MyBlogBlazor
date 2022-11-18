using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HashWorkerBlazor.Migrations
{
    public partial class _20221118v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ListItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "HashCount",
                table: "ListItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashCount",
                table: "ListItems");

            migrationBuilder.InsertData(
                table: "ListItems",
                columns: new[] { "Id", "Account", "CheckHash", "CreateTime", "FolderPath", "HashListJson", "LastSendTime" },
                values: new object[] { 1, "Test", "Test", new DateTime(2022, 11, 16, 9, 12, 22, 926, DateTimeKind.Local).AddTicks(5019), "Test", "Test", null });
        }
    }
}
