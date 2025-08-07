using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostelManagemantSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixHostelUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LicenseExpiryDate",
                table: "Hostels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HostelId",
                table: "Users",
                column: "HostelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Hostels_HostelId",
                table: "Users",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Hostels_HostelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HostelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HostelId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "LicenseExpiryDate",
                table: "Hostels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
