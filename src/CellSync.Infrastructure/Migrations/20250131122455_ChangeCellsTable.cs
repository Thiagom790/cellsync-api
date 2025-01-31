using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCellsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CellAddresses_CellId",
                table: "CellAddresses");

            migrationBuilder.CreateIndex(
                name: "IX_CellAddresses_CellId",
                table: "CellAddresses",
                column: "CellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CellAddresses_CellId",
                table: "CellAddresses");

            migrationBuilder.CreateIndex(
                name: "IX_CellAddresses_CellId",
                table: "CellAddresses",
                column: "CellId",
                unique: true);
        }
    }
}
