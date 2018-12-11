using Microsoft.EntityFrameworkCore.Migrations;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Migrations
{
    public partial class Tra6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Core",
                table: "Street");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "Core",
                table: "Street",
                nullable: true);
        }
    }
}
