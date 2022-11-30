using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunAndHikeStore.Data.Migrations
{
    public partial class AddedCustomerIdToBillingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "BillingDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BillingDetails_CustomerId",
                table: "BillingDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingDetails_AspNetUsers_CustomerId",
                table: "BillingDetails",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingDetails_AspNetUsers_CustomerId",
                table: "BillingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillingDetails_CustomerId",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "BillingDetails");

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
    }
}
