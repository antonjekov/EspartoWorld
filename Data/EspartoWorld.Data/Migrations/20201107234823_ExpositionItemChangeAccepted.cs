using Microsoft.EntityFrameworkCore.Migrations;

namespace EspartoWorld.Data.Migrations
{
    public partial class ExpositionItemChangeAccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "ExpositionItems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "ExpositionItems");
        }
    }
}
