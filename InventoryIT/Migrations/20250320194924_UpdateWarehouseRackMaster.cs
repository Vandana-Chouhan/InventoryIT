using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryIT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWarehouseRackMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Warehouse Rack Master_WarehouseAreaId",
                table: "Warehouse Rack Master",
                column: "WarehouseAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse Rack Master_WarehouseLocId",
                table: "Warehouse Rack Master",
                column: "WarehouseLocId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse Rack Master_Warehouse Area Master_WarehouseAreaId",
                table: "Warehouse Rack Master",
                column: "WarehouseAreaId",
                principalTable: "Warehouse Area Master",
                principalColumn: "WarehouseAreaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse Rack Master_Warehouse Location Master_WarehouseLocId",
                table: "Warehouse Rack Master",
                column: "WarehouseLocId",
                principalTable: "Warehouse Location Master",
                principalColumn: "WarehouseLocId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse Rack Master_Warehouse Area Master_WarehouseAreaId",
                table: "Warehouse Rack Master");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse Rack Master_Warehouse Location Master_WarehouseLocId",
                table: "Warehouse Rack Master");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse Rack Master_WarehouseAreaId",
                table: "Warehouse Rack Master");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse Rack Master_WarehouseLocId",
                table: "Warehouse Rack Master");
        }
    }
}
