using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunAndHikeStore.Data.Migrations
{
    public partial class AddedSizeIdToCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_SizeId",
                table: "CartItems",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Sizes_SizeId",
                table: "CartItems",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Sizes_SizeId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_SizeId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "CartItems");
        }
    }
}
