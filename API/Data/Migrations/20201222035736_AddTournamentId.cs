using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddTournamentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "TournamentMatchTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMatchTypes_TournamentId",
                table: "TournamentMatchTypes",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentMatchTypes_Tournament_TournamentId",
                table: "TournamentMatchTypes",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentMatchTypes_Tournament_TournamentId",
                table: "TournamentMatchTypes");

            migrationBuilder.DropIndex(
                name: "IX_TournamentMatchTypes_TournamentId",
                table: "TournamentMatchTypes");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "TournamentMatchTypes");
        }
    }
}
