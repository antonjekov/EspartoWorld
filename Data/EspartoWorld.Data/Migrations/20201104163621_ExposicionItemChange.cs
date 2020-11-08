namespace EspartoWorld.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ExposicionItemChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExposicionItems");

            migrationBuilder.CreateTable(
                name: "ExpositionItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    IsSold = table.Column<bool>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpositionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpositionItems_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpositionItems_AuthorId",
                table: "ExpositionItems",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpositionItems_IsDeleted",
                table: "ExpositionItems",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpositionItems");

            migrationBuilder.CreateTable(
                name: "ExposicionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsSold = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExposicionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExposicionItems_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExposicionItems_AuthorId",
                table: "ExposicionItems",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExposicionItems_IsDeleted",
                table: "ExposicionItems",
                column: "IsDeleted");
        }
    }
}
