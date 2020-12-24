using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedIsPlayoff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTypes_BilliardsMatchTypes_MatchTypeId",
                table: "TournamentMatchTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentModes_BilliardsModes_ModeId",
                table: "TournamentModes");

            migrationBuilder.AddColumn<bool>(
                name: "IsPlayoff",
                table: "TournamentModes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTypes_BilliardsMatchTypes_MatchTypeId",
                table: "TournamentMatchTypes",
                column: "MatchTypeId",
                principalTable: "BilliardsMatchTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentModes_BilliardsModes_ModeId",
                table: "TournamentModes",
                column: "ModeId",
                principalTable: "BilliardsModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTypes_BilliardsMatchTypes_MatchTypeId",
                table: "TournamentMatchTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentModes_BilliardsModes_ModeId",
                table: "TournamentModes");

            migrationBuilder.DropColumn(
                name: "IsPlayoff",
                table: "TournamentModes");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTypes_BilliardsMatchTypes_MatchTypeId",
                table: "TournamentMatchTypes",
                column: "MatchTypeId",
                principalTable: "BilliardsMatchTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentModes_BilliardsModes_ModeId",
                table: "TournamentModes",
                column: "ModeId",
                principalTable: "BilliardsModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
