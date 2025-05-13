using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EsmeChecker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "esmecheckerschema");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "esmecheckerschema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Administration = table.Column<string>(type: "text", nullable: true),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Emplowees",
                schema: "esmecheckerschema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Msisdn = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Postion = table.Column<string>(type: "text", nullable: true),
                    Allow = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplowees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emplowees_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "esmecheckerschema",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "esmecheckerschema",
                table: "Categories",
                columns: new[] { "Id", "Administration", "CreateDate", "Department", "ModifyDate", "Unit" },
                values: new object[,]
                {
                    { 1, "Telecom", new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4025), "Network Service", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VAS" },
                    { 2, "Commercial", new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4027), "Product Development", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VAS" }
                });

            migrationBuilder.InsertData(
                schema: "esmecheckerschema",
                table: "Emplowees",
                columns: new[] { "Id", "Allow", "CategoryId", "CreateDate", "Email", "ModifyDate", "Msisdn", "Name", "Postion" },
                values: new object[,]
                {
                    { 1, true, 1, new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4110), "m.shehob@libyana.ly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "218947776156", "Mahmood Shehob", "vas Engineer" },
                    { 2, false, 1, new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4112), "a.zeglam@libyana.ly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "218947777544", "Aisha Zeglam", "vas Engineer" },
                    { 3, true, 1, new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4113), "s.grada@libyana.ly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "218947775684", "Said Grada", "vas Engineer" },
                    { 4, true, 1, new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4114), "m.alshuhoumi@libyana.ly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "218947775683", "Makhzoum Alshuhoumi", "vas Engineer" },
                    { 5, true, 2, new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4116), "M.Elsharef@libyana.ly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "218947777131", "Mohamed Elsharef", "emplowee" },
                    { 6, true, 2, new DateTime(2025, 5, 13, 1, 20, 8, 855, DateTimeKind.Utc).AddTicks(4117), "GHADAH.ALI@Libyana.ly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "218947776081", "Ghada Ali", "emplowee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emplowees_CategoryId",
                schema: "esmecheckerschema",
                table: "Emplowees",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emplowees",
                schema: "esmecheckerschema");

            migrationBuilder.DropTable(
                name: "MaxMinConfigs",
                schema: "esmecheckerschema");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "esmecheckerschema");
        }
    }
}
