using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mealFinderDotNet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Recettes");

            migrationBuilder.RenameColumn(
                name: "Titre",
                table: "Recettes",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "ApiId",
                table: "Recettes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecetteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recettes_RecetteId",
                        column: x => x.RecetteId,
                        principalTable: "Recettes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecetteId",
                table: "Ingredients",
                column: "RecetteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ApiId",
                table: "Recettes");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Recettes",
                newName: "Titre");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Recettes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
