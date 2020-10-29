using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EspartoWorld.Data.Migrations
{
    public partial class Course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Place = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    GroupSize = table.Column<int>(nullable: false),
                    LengthInDays = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Organizer = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    HaveFreePlaces = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IsDeleted",
                table: "Courses",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
