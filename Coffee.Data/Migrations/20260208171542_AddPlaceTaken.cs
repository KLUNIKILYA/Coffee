using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaceTaken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlacesTaken",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacesTaken",
                table: "Events");
        }
    }
}
