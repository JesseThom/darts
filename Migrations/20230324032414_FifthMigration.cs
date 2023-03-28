using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace darts.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Week",
                table: "Players",
                newName: "PlayerPoints");

            migrationBuilder.RenameColumn(
                name: "PlayersId",
                table: "Players",
                newName: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerPoints",
                table: "Players",
                newName: "Week");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Players",
                newName: "PlayersId");
        }
    }
}
