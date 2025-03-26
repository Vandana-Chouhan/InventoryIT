using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryIT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWarehouseAreaMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Warehouse Area Master_WarehouseLocId",
                table: "Warehouse Area Master",
                column: "WarehouseLocId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse Area Master_Warehouse Location Master_WarehouseLocId",
                table: "Warehouse Area Master",
                column: "WarehouseLocId",
                principalTable: "Warehouse Location Master",
                principalColumn: "WarehouseLocId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse Area Master_Warehouse Location Master_WarehouseLocId",
                table: "Warehouse Area Master");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse Area Master_WarehouseLocId",
                table: "Warehouse Area Master");
        }
    }
}
