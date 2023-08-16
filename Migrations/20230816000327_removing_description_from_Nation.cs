using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace African_Football_Legends.Migrations
{
    /// <inheritdoc />
    public partial class removing_description_from_Nation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Nations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Nations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
