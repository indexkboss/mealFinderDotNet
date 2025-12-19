using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mealFinderDotNet.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameAndFirebaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateInscription",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Utilisateurs");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Utilisateurs",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "MotDePasse",
                table: "Utilisateurs",
                newName: "FirebaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Utilisateurs",
                newName: "Nom");

            migrationBuilder.RenameColumn(
                name: "FirebaseId",
                table: "Utilisateurs",
                newName: "MotDePasse");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInscription",
                table: "Utilisateurs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
