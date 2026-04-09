using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetStore.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelAfterMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShelterName",
                table: "ApplicationForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ApplicationForms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PhoneNumber", "ShelterName" },
                values: new object[] { "555-123-4567", "Golden Paws Rescue" });

            migrationBuilder.UpdateData(
                table: "ApplicationForms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PhoneNumber", "ShelterName" },
                values: new object[] { "555-567-8901", "Whisker Haven" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShelterName",
                table: "ApplicationForms");

            migrationBuilder.UpdateData(
                table: "ApplicationForms",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhoneNumber",
                value: "555-1234");

            migrationBuilder.UpdateData(
                table: "ApplicationForms",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhoneNumber",
                value: "555-5678");
        }
    }
}
