using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mealFinderDotNet.Migrations
{
    /// <inheritdoc />
    public partial class FinalCleanSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaires");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Recherches");

            migrationBuilder.DropTable(
                name: "Statistiques");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commentaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCommentaire = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEnvoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstLue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recherches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRecherche = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MotCles = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recherches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistiques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalFavoris = table.Column<int>(type: "int", nullable: false),
                    TotalRecherches = table.Column<int>(type: "int", nullable: false),
                    TotalUtilisateurs = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistiques", x => x.Id);
                });
        }
    }
}
