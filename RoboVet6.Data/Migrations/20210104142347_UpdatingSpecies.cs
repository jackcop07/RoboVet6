using Microsoft.EntityFrameworkCore.Migrations;

namespace RoboVet6.Data.Migrations
{
    public partial class UpdatingSpecies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpeciesModelId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SpeciesModelId",
                table: "Animals",
                column: "SpeciesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Species_SpeciesModelId",
                table: "Animals",
                column: "SpeciesModelId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Species_SpeciesModelId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_SpeciesModelId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SpeciesModelId",
                table: "Animals");
        }
    }
}
