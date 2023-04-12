using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HashWorkerBlazor.Migrations
{
    public partial class _20230412v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64Img",
                table: "ListItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Img",
                table: "ListItems");
        }
    }
}
