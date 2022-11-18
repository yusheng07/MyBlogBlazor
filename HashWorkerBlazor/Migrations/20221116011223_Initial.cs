using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HashWorkerBlazor.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Account = table.Column<string>(type: "TEXT", nullable: false),
                    FolderPath = table.Column<string>(type: "TEXT", nullable: false),
                    HashListJson = table.Column<string>(type: "TEXT", nullable: false),
                    CheckHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastSendTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ListItems",
                columns: new[] { "Id", "Account", "CheckHash", "CreateTime", "FolderPath", "HashListJson", "LastSendTime" },
                values: new object[] { 1, "Test", "Test", new DateTime(2022, 11, 16, 9, 12, 22, 926, DateTimeKind.Local).AddTicks(5019), "Test", "Test", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListItems");
        }
    }
}
