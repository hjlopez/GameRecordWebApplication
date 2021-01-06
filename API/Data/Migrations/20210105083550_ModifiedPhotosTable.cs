using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ModifiedPhotosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId1",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_AppUserId1",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Photos");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId1",
                table: "Photos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId1",
                table: "Photos",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId1",
                table: "Photos",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
