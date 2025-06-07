using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ImageMapper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products",
                column: "ThumbnailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ImageId",
                table: "ProductCategories",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ImageId",
                table: "Brands",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Images_ImageId",
                table: "Brands",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Images_ImageId",
                table: "ProductCategories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ThumbnailId",
                table: "Products",
                column: "ThumbnailId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Images_ImageId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Images_ImageId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ThumbnailId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ImageId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ImageId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Brands");
        }
    }
}
