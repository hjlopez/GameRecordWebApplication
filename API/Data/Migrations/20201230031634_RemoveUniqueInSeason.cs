using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class RemoveUniqueInSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Seasons_TournamentId",
                table: "Seasons");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_TournamentId",
                table: "Seasons",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Seasons_TournamentId",
                table: "Seasons");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_TournamentId",
                table: "Seasons",
                column: "TournamentId",
                unique: true);
        }
    }
}
