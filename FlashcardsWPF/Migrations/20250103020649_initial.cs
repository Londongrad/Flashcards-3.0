using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcards.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(777)", maxLength: 777, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(777)", maxLength: 777, nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsLastWord = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sets_Name",
                table: "Sets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_Name",
                table: "Words",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_SetId",
                table: "Words",
                column: "SetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Sets");
        }
    }
}
