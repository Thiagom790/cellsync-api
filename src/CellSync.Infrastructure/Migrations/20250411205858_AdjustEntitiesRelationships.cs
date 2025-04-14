using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustEntitiesRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Members",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Members",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Meetings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LeaderId",
                table: "Meetings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Meetings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cells",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentLeaderId",
                table: "Cells",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cells",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CellsLeaderHistory",
                columns: table => new
                {
                    CellId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellsLeaderHistory", x => new { x.CellId, x.LeaderId });
                    table.ForeignKey(
                        name: "FK_CellsLeaderHistory_Cells_CellId",
                        column: x => x.CellId,
                        principalTable: "Cells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CellsLeaderHistory_Members_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LeaderId",
                table: "Meetings",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cells_CurrentLeaderId",
                table: "Cells",
                column: "CurrentLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_CellsLeaderHistory_LeaderId",
                table: "CellsLeaderHistory",
                column: "LeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Members_CurrentLeaderId",
                table: "Cells",
                column: "CurrentLeaderId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Members_LeaderId",
                table: "Meetings",
                column: "LeaderId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Members_CurrentLeaderId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Members_LeaderId",
                table: "Meetings");

            migrationBuilder.DropTable(
                name: "CellsLeaderHistory");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_LeaderId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Cells_CurrentLeaderId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "CurrentLeaderId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cells");
        }
    }
}
