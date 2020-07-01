using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPProjekat.EFDataAccess.Migrations
{
    public partial class addedcarttoarticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 30, 12, 8, 9, 560, DateTimeKind.Local).AddTicks(3881));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 30, 12, 8, 9, 573, DateTimeKind.Local).AddTicks(1298));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 30, 12, 8, 9, 573, DateTimeKind.Local).AddTicks(1609));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 29, 12, 12, 22, 861, DateTimeKind.Local).AddTicks(334));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 29, 12, 12, 23, 2, DateTimeKind.Local).AddTicks(5720));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 29, 12, 12, 23, 2, DateTimeKind.Local).AddTicks(5918));
        }
    }
}
