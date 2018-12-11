using Microsoft.EntityFrameworkCore.Migrations;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Migrations
{
    public partial class Tra5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Street",
                schema: "Core",
                table: "Street");

            migrationBuilder.DropIndex(
                name: "IX_Street_AddressId",
                schema: "Core",
                table: "Street");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Street",
                schema: "Core",
                table: "Street",
                columns: new[] { "AddressId", "StreetName", "StreetNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Street",
                schema: "Core",
                table: "Street");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Street",
                schema: "Core",
                table: "Street",
                columns: new[] { "StreetName", "StreetNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Street_AddressId",
                schema: "Core",
                table: "Street",
                column: "AddressId");
        }
    }
}
