using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetEnglishP5.Data.Migrations
{
    /// <inheritdoc />
    public partial class TwelfthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Car_CarId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Car_CarId",
                table: "Image",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Car_CarId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Car_CarId",
                table: "Image",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
