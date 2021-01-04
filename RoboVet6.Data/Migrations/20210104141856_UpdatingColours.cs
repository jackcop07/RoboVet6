using Microsoft.EntityFrameworkCore.Migrations;

namespace RoboVet6.Data.Migrations
{
    public partial class UpdatingColours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BreedModelId",
                table: "Colours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpeciesModelId",
                table: "Breeds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colours_BreedModelId",
                table: "Colours",
                column: "BreedModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Breeds_SpeciesModelId",
                table: "Breeds",
                column: "SpeciesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breeds_Species_SpeciesModelId",
                table: "Breeds",
                column: "SpeciesModelId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Colours_Breeds_BreedModelId",
                table: "Colours",
                column: "BreedModelId",
                principalTable: "Breeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breeds_Species_SpeciesModelId",
                table: "Breeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Colours_Breeds_BreedModelId",
                table: "Colours");

            migrationBuilder.DropIndex(
                name: "IX_Colours_BreedModelId",
                table: "Colours");

            migrationBuilder.DropIndex(
                name: "IX_Breeds_SpeciesModelId",
                table: "Breeds");

            migrationBuilder.DropColumn(
                name: "BreedModelId",
                table: "Colours");

            migrationBuilder.DropColumn(
                name: "SpeciesModelId",
                table: "Breeds");
        }
    }
}
