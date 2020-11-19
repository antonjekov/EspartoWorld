namespace EspartoWorld.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ManufacturerProductModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpositionItems_Manufacturers_ManufacturerId",
                table: "ExpositionItems");

            migrationBuilder.DropIndex(
                name: "IX_ExpositionItems_ManufacturerId",
                table: "ExpositionItems");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "ExpositionItems");

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ManufacturerId",
                table: "ExpositionItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpositionItems_ManufacturerId",
                table: "ExpositionItems",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpositionItems_Manufacturers_ManufacturerId",
                table: "ExpositionItems",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
