using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class ChangeShotEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShotByPlayerName",
                table: "Shots");

            migrationBuilder.AddColumn<Guid>(
                name: "ShotByPlayerId",
                table: "Shots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Shots_ShotByPlayerId",
                table: "Shots",
                column: "ShotByPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_NextTurnPlayerId",
                table: "Games",
                column: "NextTurnPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_NextTurnPlayerId",
                table: "Games",
                column: "NextTurnPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shots_Players_ShotByPlayerId",
                table: "Shots",
                column: "ShotByPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_NextTurnPlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Shots_Players_ShotByPlayerId",
                table: "Shots");

            migrationBuilder.DropIndex(
                name: "IX_Shots_ShotByPlayerId",
                table: "Shots");

            migrationBuilder.DropIndex(
                name: "IX_Games_NextTurnPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ShotByPlayerId",
                table: "Shots");

            migrationBuilder.AddColumn<string>(
                name: "ShotByPlayerName",
                table: "Shots",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
