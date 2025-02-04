using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GHWalk.Migrations
{
    /// <inheritdoc />
    public partial class difficultyseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0f142854-f595-4339-1474-08dd44f91363"), "Easy" },
                    { new Guid("5d3b713f-b458-4fdc-8394-08dd4501ec6a"), "" },
                    { new Guid("66a7ef7f-c0a1-4312-38de-08dd45336635"), "Hard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0f142854-f595-4339-1474-08dd44f91363"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5d3b713f-b458-4fdc-8394-08dd4501ec6a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("66a7ef7f-c0a1-4312-38de-08dd45336635"));
        }
    }
}
