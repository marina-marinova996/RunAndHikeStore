using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunAndHikeStore.Data.Migrations
{
    public partial class AddedBillingDetailsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BillingDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillingDetails_ApplicationUserId",
                table: "BillingDetails",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingDetails_AspNetUsers_ApplicationUserId",
                table: "BillingDetails",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingDetails_AspNetUsers_ApplicationUserId",
                table: "BillingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillingDetails_ApplicationUserId",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BillingDetails");
        }
    }
}
