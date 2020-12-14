using Microsoft.EntityFrameworkCore.Migrations;

namespace RoboVet6.Data.Migrations
{
    public partial class workphone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkPhone",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkPhone",
                table: "Clients");
        }
    }
}
