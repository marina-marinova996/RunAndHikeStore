using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunAndHikeStore.Data.Migrations
{
    public partial class ChangedCartItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartItems_IsDeleted",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_IsDeleted",
                table: "CartItems",
                column: "IsDeleted");
        }
    }
}
