using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetEnglishP5.Data.Migrations
{
    /// <inheritdoc />
    public partial class EleventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Image",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
