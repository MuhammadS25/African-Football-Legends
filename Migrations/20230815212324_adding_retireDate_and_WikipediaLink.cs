using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace African_Football_Legends.Migrations
{
    /// <inheritdoc />
    public partial class adding_retireDate_and_WikipediaLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "RetireDate",
                table: "Players",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "WikipediaLink",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetireDate",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WikipediaLink",
                table: "Players");
        }
    }
}
