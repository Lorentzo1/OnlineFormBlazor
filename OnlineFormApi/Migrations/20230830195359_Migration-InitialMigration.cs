using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineFormApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GdprApproved = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonRole",
                columns: table => new
                {
                    PersonsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRole", x => new { x.PersonsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PersonRole_Person_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOn", "RoleName" },
                values: new object[,]
                {
                    { -6, new DateTime(2023, 8, 30, 21, 53, 59, 835, DateTimeKind.Local).AddTicks(4633), "Global Auditor" },
                    { -5, new DateTime(2023, 8, 30, 21, 53, 59, 835, DateTimeKind.Local).AddTicks(4621), "System Auditor" },
                    { -4, new DateTime(2023, 8, 30, 21, 53, 59, 835, DateTimeKind.Local).AddTicks(4611), "Page Administrator" },
                    { -3, new DateTime(2023, 8, 30, 21, 53, 59, 835, DateTimeKind.Local).AddTicks(4601), "Active Directory Administrator" },
                    { -2, new DateTime(2023, 8, 30, 21, 53, 59, 835, DateTimeKind.Local).AddTicks(4588), "Role Administrator" },
                    { -1, new DateTime(2023, 8, 30, 21, 53, 59, 835, DateTimeKind.Local).AddTicks(4515), "System Administrator 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonRole_RolesId",
                table: "PersonRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRole");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
