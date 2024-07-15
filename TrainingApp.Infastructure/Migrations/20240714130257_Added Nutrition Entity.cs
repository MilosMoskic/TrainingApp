using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNutritionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nutritions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ActivityLevel = table.Column<int>(type: "int", nullable: false),
                    BMR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TDEE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Calories = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Carbs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutritions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nutritions");
        }
    }
}
