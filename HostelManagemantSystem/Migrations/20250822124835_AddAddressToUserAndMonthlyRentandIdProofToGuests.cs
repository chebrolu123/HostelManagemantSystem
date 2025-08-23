using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostelManagemantSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToUserAndMonthlyRentandIdProofToGuests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdProof",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MothlyRent",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdProof",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "MothlyRent",
                table: "Guests");
        }
    }
}
