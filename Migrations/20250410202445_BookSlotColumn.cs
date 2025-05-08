using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocBookAPI.Migrations
{
    /// <inheritdoc />
    public partial class BookSlotColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookedSlots",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedSlots",
                table: "Appointments");
        }
    }
}
