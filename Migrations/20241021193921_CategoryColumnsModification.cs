using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.Migrations
{
    /// <inheritdoc />
    public partial class CategoryColumnsModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TemporaryLessonAcessType",
                table: "TemporaryLessonsDetails",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TemporaryLessonAcessType",
                table: "TemporaryLessonsDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");
        }
    }
}
