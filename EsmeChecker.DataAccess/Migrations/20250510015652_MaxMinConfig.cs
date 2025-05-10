using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EsmeChecker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MaxMinConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Allow",
                schema: "esmecheckerschema",
                table: "Emplowees",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaxMinConfigs",
                schema: "esmecheckerschema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FixedHourMax = table.Column<int>(type: "integer", nullable: false),
                    FixedHourMin = table.Column<int>(type: "integer", nullable: false),
                    DayMax = table.Column<int>(type: "integer", nullable: false),
                    DayMin = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaxMinConfigs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Allow",
                value: null);

            migrationBuilder.UpdateData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Allow",
                value: null);

            migrationBuilder.UpdateData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Allow",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaxMinConfigs",
                schema: "esmecheckerschema");

            migrationBuilder.DropColumn(
                name: "Allow",
                schema: "esmecheckerschema",
                table: "Emplowees");
        }
    }
}
