using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class FixBrandAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "InStock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Brands");

            migrationBuilder.AlterColumn<int>(
                name: "ThumbnailId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ProductCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Brands",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products",
                column: "ThumbnailId",
                unique: true,
                filter: "[ThumbnailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ImageId",
                table: "ProductCategories",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ImageId",
                table: "Brands",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Images_ImageId",
                table: "Brands",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Images_ImageId",
                table: "ProductCategories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ThumbnailId",
                table: "Products",
                column: "ThumbnailId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.AlterColumn<int>(
                name: "ThumbnailId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
