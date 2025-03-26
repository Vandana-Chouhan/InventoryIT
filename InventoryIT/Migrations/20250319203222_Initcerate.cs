using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryIT.Migrations
{
    /// <inheritdoc />
    public partial class Initcerate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item Master",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(name: "Item Name", type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    ItemCode = table.Column<string>(name: "Item Code", type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    ItemType = table.Column<int>(name: "Item Type", type: "int", nullable: false),
                    ItemCompany = table.Column<int>(name: "Item Company", type: "int", nullable: false),
                    ItemCatagory = table.Column<int>(name: "Item Catagory", type: "int", nullable: false),
                    ItemSubCatagory = table.Column<int>(name: "Item SubCatagory", type: "int", nullable: false),
                    ItemUnit1 = table.Column<int>(name: "[Item Unit1", type: "int", nullable: false),
                    ItemUnit2 = table.Column<int>(name: "Item Unit2", type: "int", nullable: false),
                    WarehouseLocation = table.Column<int>(name: "Warehouse Location", type: "int", nullable: false),
                    WarehouseArea = table.Column<int>(name: "Warehouse Area", type: "int", nullable: false),
                    WarehouseRack = table.Column<int>(name: "Warehouse Rack", type: "int", nullable: false),
                    WarehouseShelf = table.Column<int>(name: "Warehouse Shelf", type: "int", nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    FinanYearId = table.Column<int>(type: "int", nullable: true),
                    PartNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item Master", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Supplier Master",
                columns: table => new
                {
                    SuppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(name: "Supplier Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ContactPerson = table.Column<string>(name: "Contact Person", type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SupplierGSTCertificate = table.Column<byte[]>(name: "Supplier GST Certificate", type: "varbinary(max)", nullable: true),
                    PINNo = table.Column<string>(name: "PIN No", type: "varchar(7)", unicode: false, maxLength: 7, nullable: true),
                    GSTNo = table.Column<string>(name: "GST No", type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    MobileNo = table.Column<decimal>(name: "Mobile No", type: "numeric(12,0)", nullable: true),
                    PhoneNo = table.Column<decimal>(name: "Phone No", type: "numeric(12,0)", nullable: true),
                    EmailId = table.Column<string>(name: "Email Id", type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CompId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    Creation_DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_By = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier Master", x => x.SuppId);
                });

            migrationBuilder.CreateTable(
                name: "Mast_ItemSTK",
                columns: table => new
                {
                    StockId = table.Column<int>(name: "Stock Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    OpeningQuantity = table.Column<double>(name: "Opening Quantity", type: "float", nullable: true),
                    CurrentQuantity = table.Column<double>(name: "Current Quantity", type: "float", nullable: true),
                    ClosingQuantity = table.Column<double>(name: "Closing Quantity", type: "float", nullable: true),
                    OpeningValue = table.Column<double>(name: "Opening Value", type: "float", nullable: true),
                    GST = table.Column<double>(name: "GST%", type: "float", nullable: true),
                    PurchaseRate = table.Column<double>(name: "Purchase Rate", type: "float", nullable: true),
                    SalesRate = table.Column<double>(name: "Sales Rate", type: "float", nullable: true),
                    BufferStock = table.Column<double>(name: "Buffer Stock", type: "float", nullable: true),
                    CompId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    FinanYearId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: true),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mast_ItemSTK", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item Master",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateTable(
                name: "Mast_ItemSupplierRate",
                columns: table => new
                {
                    SuppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryId = table.Column<int>(type: "int", nullable: false),
                    SupplierRate = table.Column<double>(name: "Supplier Rate", type: "float", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    SupplierCompanyName = table.Column<string>(name: "Supplier Company  Name", type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    SupplierName = table.Column<string>(name: "Supplier Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mast_ItemSupplierRate", x => x.SuppId);
                    table.ForeignKey(
                        name: "FK_ItemId_ItemSupplier",
                        column: x => x.ItemId,
                        principalTable: "Item Master",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mast_ItemSTK_ItemId",
                table: "Mast_ItemSTK",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Mast_ItemSupplierRate_ItemId",
                table: "Mast_ItemSupplierRate",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mast_ItemSTK");

            migrationBuilder.DropTable(
                name: "Mast_ItemSupplierRate");

            migrationBuilder.DropTable(
                name: "Supplier Master");

            migrationBuilder.DropTable(
                name: "Item Master");
        }
    }
}
