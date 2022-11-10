using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmtsChecker.Migrations
{
    public partial class _202211091 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Hosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts",
                column: "Hostname");

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Hostname", "Ip" },
                values: new object[] { "PC01", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Hostname", "Ip" },
                values: new object[] { "PC02", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Hostname", "Ip" },
                values: new object[] { "PC03", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts");

            migrationBuilder.DeleteData(
                table: "Hosts",
                keyColumn: "Hostname",
                keyValue: "PC01");

            migrationBuilder.DeleteData(
                table: "Hosts",
                keyColumn: "Hostname",
                keyValue: "PC02");

            migrationBuilder.DeleteData(
                table: "Hosts",
                keyColumn: "Hostname",
                keyValue: "PC03");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Hosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts",
                column: "Id");
        }
    }
}
