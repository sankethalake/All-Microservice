using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerMicroService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    SportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportName = table.Column<string>(nullable: false),
                    NoOfPlayers = table.Column<int>(nullable: false),
                    SportType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.SportId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportId = table.Column<int>(nullable: false),
                    PlayerName = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_SportId",
                table: "Players",
                column: "SportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
