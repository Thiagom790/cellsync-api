using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustMeetingEntitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLeader",
                table: "MeetingMembers");

            migrationBuilder.AddColumn<Guid>(
                name: "CellId",
                table: "Meetings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CellId",
                table: "Meetings",
                column: "CellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Cells_CellId",
                table: "Meetings",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Cells_CellId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CellId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CellId",
                table: "Meetings");

            migrationBuilder.AddColumn<bool>(
                name: "IsLeader",
                table: "MeetingMembers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
