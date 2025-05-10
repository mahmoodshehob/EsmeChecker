using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EsmeChecker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedEmplowee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                columns: new[] { "Id", "CategoryId", "CreateDate", "Email", "ModifyDate", "Msisdn", "Name", "Postion" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 4, 26, 21, 6, 50, 0, DateTimeKind.Utc), "m.shehob@libyana.ly", new DateTime(2025, 4, 26, 21, 6, 50, 0, DateTimeKind.Utc), "218947776156", "Mahmood Shehob", "vas Engineer" },
                    { 2, 1, new DateTime(2025, 4, 26, 21, 6, 50, 0, DateTimeKind.Utc), "s.grada@libyana.ly", new DateTime(2025, 4, 26, 21, 6, 50, 0, DateTimeKind.Utc), "218947775684", "Said Grada", "vas Engineer" },
                    { 3, 2, new DateTime(2025, 4, 26, 21, 6, 50, 0, DateTimeKind.Utc), "s.grada@libyana.ly", new DateTime(2025, 4, 26, 21, 6, 50, 0, DateTimeKind.Utc), "218947776081", "Ghada", "vas Engineer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
