using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "ProductPictures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_BlogId",
                table: "ProductPictures",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_Blogs_BlogId",
                table: "ProductPictures",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_Blogs_BlogId",
                table: "ProductPictures");

            migrationBuilder.DropIndex(
                name: "IX_ProductPictures_BlogId",
                table: "ProductPictures");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "ProductPictures");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
