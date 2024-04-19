using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaloriesCounterAPI.Migrations
{
    /// <inheritdoc />
    public partial class catbs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarbsForMeal",
                table: "Meal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FatForMeal",
                table: "Meal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProteinForMeal",
                table: "Meal",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbsForMeal",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "FatForMeal",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "ProteinForMeal",
                table: "Meal");
        }
    }
}
