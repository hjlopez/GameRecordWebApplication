using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class BilliardsModeAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BilliardsMatchTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilliardsMatchTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BilliardsModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Mode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilliardsModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentMatchTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentMatchTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentMatchTypes_BilliardsMatchTypes_MatchTypeId",
                        column: x => x.MatchTypeId,
                        principalTable: "BilliardsMatchTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLast = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsConsolation = table.Column<bool>(type: "INTEGER", nullable: false),
                    HighestRank = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentModes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentModes_BilliardsModes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "BilliardsModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMatchTypes_MatchTypeId",
                table: "TournamentMatchTypes",
                column: "MatchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentModes_HighestRank",
                table: "TournamentModes",
                column: "HighestRank",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentModes_ModeId",
                table: "TournamentModes",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentModes_Order",
                table: "TournamentModes",
                column: "Order",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentMatchTypes");

            migrationBuilder.DropTable(
                name: "TournamentModes");

            migrationBuilder.DropTable(
                name: "BilliardsMatchTypes");

            migrationBuilder.DropTable(
                name: "BilliardsModes");
        }
    }
}
