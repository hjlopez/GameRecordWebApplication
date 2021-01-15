using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Data.Migrations
{
    public partial class PostgresInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GamerTag = table.Column<string>(type: "text", nullable: true),
                    PlayMH = table.Column<bool>(type: "boolean", nullable: false),
                    PlayDota = table.Column<bool>(type: "boolean", nullable: false),
                    JoinBilliards = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BilliardsMatchTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilliardsMatchTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BilliardsModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilliardsModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LoggedOutLink = table.Column<string>(type: "text", nullable: false),
                    LoggedInLink = table.Column<string>(type: "text", nullable: false),
                    GameTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TournamentName = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournament_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsLast = table.Column<bool>(type: "boolean", nullable: false),
                    IsConsolation = table.Column<bool>(type: "boolean", nullable: false),
                    HighestRank = table.Column<int>(type: "integer", nullable: false),
                    IsPlayoff = table.Column<bool>(type: "boolean", nullable: false),
                    ModeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentModes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentModes_BilliardsModes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "BilliardsModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTypes_Games_Id",
                        column: x => x.Id,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeasonNumber = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentMatchTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    MatchTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentMatchTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentMatchTypes_BilliardsMatchTypes_MatchTypeId",
                        column: x => x.MatchTypeId,
                        principalTable: "BilliardsMatchTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentMatchTypes_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentMembers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentMembers_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BilliardsMatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WinUserId = table.Column<int>(type: "integer", nullable: false),
                    LoseUserId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    ModeId = table.Column<int>(type: "integer", nullable: false),
                    SeasonNumberId = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    WinnerWins = table.Column<int>(type: "integer", nullable: false),
                    LoserWins = table.Column<int>(type: "integer", nullable: false),
                    TotalGamesPlayed = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "SeasonHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeasonNumberId = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    ModeId = table.Column<int>(type: "integer", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeasonHistory_BilliardsMatchTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BilliardsMatchTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeasonHistory_Seasons_SeasonNumberId",
                        column: x => x.SeasonNumberId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeasonHistory_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonHistory_SeasonNumberId",
                table: "SeasonHistory",
                column: "SeasonNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonHistory_TournamentId",
                table: "SeasonHistory",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonHistory_TypeId",
                table: "SeasonHistory",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonHistory_UserId",
                table: "SeasonHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_TournamentId",
                table: "Seasons",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_AppUserId",
                table: "Tournament",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMatchTypes_MatchTypeId",
                table: "TournamentMatchTypes",
                column: "MatchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMatchTypes_TournamentId",
                table: "TournamentMatchTypes",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMembers_AppUserId",
                table: "TournamentMembers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentMembers_TournamentId",
                table: "TournamentMembers",
                column: "TournamentId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BilliardsMatch");

            migrationBuilder.DropTable(
                name: "GameTypes");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "SeasonHistory");

            migrationBuilder.DropTable(
                name: "TournamentMatchTypes");

            migrationBuilder.DropTable(
                name: "TournamentMembers");

            migrationBuilder.DropTable(
                name: "TournamentModes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "BilliardsMatchTypes");

            migrationBuilder.DropTable(
                name: "BilliardsModes");

            migrationBuilder.DropTable(
                name: "Tournament");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
