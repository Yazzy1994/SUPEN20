using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SUPEN20DB.Migrations
{
    public partial class Initial01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderItems_OrderItemProductId_OrderItemOrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderItemProductId_OrderItemOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderItemOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderItemProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderItems");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderItemOrderId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderItemProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "OrderItems",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderItemProductId_OrderItemOrderId",
                table: "Products",
                columns: new[] { "OrderItemProductId", "OrderItemOrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderItems_OrderItemProductId_OrderItemOrderId",
                table: "Products",
                columns: new[] { "OrderItemProductId", "OrderItemOrderId" },
                principalTable: "OrderItems",
                principalColumns: new[] { "ProductId", "OrderId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
