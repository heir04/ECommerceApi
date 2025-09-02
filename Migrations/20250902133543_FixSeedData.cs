using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("62022656-52a0-acb1-31db-d86472404170"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c2e3433b-f76e-56b1-28dc-757cb0daff13"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f1ce9287-ed2b-154d-33d3-7e3741c8426c"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsDeleted", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "A stylish shoe", false, "Shoe", 10.0m, 5 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "A classic wristwatch", false, "Wristwatch", 20.0m, 5 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "A durable backpack", false, "Backpack", 30.0m, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsDeleted", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("62022656-52a0-acb1-31db-d86472404170"), null, false, "Wristwatch", 20.0m, 0 },
                    { new Guid("c2e3433b-f76e-56b1-28dc-757cb0daff13"), null, false, "Shoe", 10.0m, 0 },
                    { new Guid("f1ce9287-ed2b-154d-33d3-7e3741c8426c"), null, false, "Backpack", 30.0m, 0 }
                });
        }
    }
}
