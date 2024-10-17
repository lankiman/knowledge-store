using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModelsForThumbnailAndProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaylistThumbnailUrl",
                table: "Playlists",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LessonThumbnailUrl",
                table: "Lessons",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicUrl",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaylistThumbnailUrl",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "LessonThumbnailUrl",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ProfilePicUrl",
                table: "AspNetUsers");
        }
    }
}
