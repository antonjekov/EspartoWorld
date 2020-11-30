using Microsoft.EntityFrameworkCore.Migrations;

namespace EspartoWorld.Data.Migrations
{
    public partial class UserCoursesCourseUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserCourse",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCourse", x => new { x.CoursesId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCourse_ParticipantsId",
                table: "ApplicationUserCourse",
                column: "ParticipantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserCourse");
        }
    }
}
