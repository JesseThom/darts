using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace darts.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Leagues_LeagueId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LeagueId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LeagueId",
                table: "Users",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Leagues_LeagueId",
                table: "Users",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "LeagueId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
