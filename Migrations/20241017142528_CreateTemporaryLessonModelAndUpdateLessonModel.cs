using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.Migrations
{
    /// <inheritdoc />
    public partial class CreateTemporaryLessonModelAndUpdateLessonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TemporaryLessons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedAt = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UpdatedAt = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TempLessonUrl = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LessonOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonVideoStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryLessons_Instructors_LessonOwnerId",
                        column: x => x.LessonOwnerId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryLessonsDetails",
                columns: table => new
                {
                    TemporaryLessonDetialsId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TemporaryLessonName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TemporaryLessonDescription = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    TemporaryLessonCategory = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TemporaryLessonThumbnailUrl = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TemporaryLessonAcessType = table.Column<int>(type: "int", nullable: false),
                    TemporaryLessonId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryLessonsDetails", x => x.TemporaryLessonDetialsId);
                    table.ForeignKey(
                        name: "FK_TemporaryLessonsDetails_TemporaryLessons_TemporaryLessonId",
                        column: x => x.TemporaryLessonId,
                        principalTable: "TemporaryLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryLessons_LessonOwnerId",
                table: "TemporaryLessons",
                column: "LessonOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryLessonsDetails_TemporaryLessonId",
                table: "TemporaryLessonsDetails",
                column: "TemporaryLessonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporaryLessonsDetails");

            migrationBuilder.DropTable(
                name: "TemporaryLessons");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Lessons");
        }
    }
}
