using Microsoft.EntityFrameworkCore.Migrations;

namespace RoboVet6.Data.Migrations
{
    public partial class UpdatingBreeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Colours_BreedId",
                table: "Colours",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Breeds_SpeciesId",
                table: "Breeds",
                column: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breeds_Species_SpeciesId",
                table: "Breeds",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Colours_Breeds_BreedId",
                table: "Colours",
                column: "BreedId",
                principalTable: "Breeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breeds_Species_SpeciesId",
                table: "Breeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Colours_Breeds_BreedId",
                table: "Colours");

            migrationBuilder.DropIndex(
                name: "IX_Colours_BreedId",
                table: "Colours");

            migrationBuilder.DropIndex(
                name: "IX_Breeds_SpeciesId",
                table: "Breeds");

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
    }
}
