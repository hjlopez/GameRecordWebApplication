using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedMatchTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BilliardsMatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WinUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoseUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonNumberId = table.Column<int>(type: "INTEGER", nullable: false),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnerWins = table.Column<int>(type: "INTEGER", nullable: false),
                    LoserWins = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalGamesPlayed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilliardsMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BilliardsMatch_AspNetUsers_LoseUserId",
                        column: x => x.LoseUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BilliardsMatch_AspNetUsers_WinUserId",
                        column: x => x.WinUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BilliardsMatch_BilliardsMatchTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BilliardsMatchTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BilliardsMatch_BilliardsModes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "BilliardsModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BilliardsMatch_Seasons_SeasonNumberId",
                        column: x => x.SeasonNumberId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BilliardsMatch_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BilliardsMatch_LoseUserId",
                table: "BilliardsMatch",
                column: "LoseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BilliardsMatch_ModeId",
                table: "BilliardsMatch",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_BilliardsMatch_SeasonNumberId",
                table: "BilliardsMatch",
                column: "SeasonNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_BilliardsMatch_TournamentId",
                table: "BilliardsMatch",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_BilliardsMatch_TypeId",
                table: "BilliardsMatch",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BilliardsMatch_WinUserId",
                table: "BilliardsMatch",
                column: "WinUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BilliardsMatch");
        }
    }
}
