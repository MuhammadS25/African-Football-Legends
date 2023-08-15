using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace African_Football_Legends.Migrations
{
    /// <inheritdoc />
    public partial class adding_shirtNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Shirt_Number",
                table: "Players",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shirt_Number",
                table: "Players");
        }
    }
}
