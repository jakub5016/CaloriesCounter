using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CaloriesCounterAPI.Migrations
{
    /// <inheritdoc />
    public partial class Better : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAdded");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Protein",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Meal",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MealId",
                table: "Product",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Meal_MealId",
                table: "Product",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Meal_MealId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_MealId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Meal",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "ProductAdded",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MealId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdded", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdded_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAdded_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdded_MealId",
                table: "ProductAdded",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdded_ProductId",
                table: "ProductAdded",
                column: "ProductId");
        }
    }
}
