using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changedandremovedsomeproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "Wods");

            migrationBuilder.AddColumn<string>(
                name: "WOD",
                table: "Wods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WOD",
                table: "Wods");

            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "Wods",
                type: "bit",
                nullable: true);
        }
    }
}
