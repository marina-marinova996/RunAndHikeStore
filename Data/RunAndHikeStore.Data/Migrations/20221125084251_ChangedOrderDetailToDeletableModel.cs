using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace RunAndHikeStore.Data.Migrations
{
    public partial class ChangedOrderDetailToDeletableModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_IsDeleted",
                table: "OrderDetails",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_IsDeleted",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderDetails");
        }
    }
}
