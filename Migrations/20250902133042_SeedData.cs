using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
