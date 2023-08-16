using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace African_Football_Legends.Migrations
{
    /// <inheritdoc />
    public partial class adding_internationalGoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "International_Goals",
                table: "Players",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "International_Goals",
                table: "Players");
        }
    }
}
