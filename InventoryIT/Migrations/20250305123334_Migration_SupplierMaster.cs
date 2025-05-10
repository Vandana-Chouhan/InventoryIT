using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryIT.Migrations
{
    /// <inheritdoc />
    public partial class Migration_SupplierMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item Catagory",
                columns: table => new
                {
                    ItemCatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCatagoryName = table.Column<string>(name: "Item Catagory Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item Catagory", x => x.ItemCatId);
                });

            migrationBuilder.CreateTable(
                name: "ItemCompany",
                columns: table => new
                {
                    ItemComId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCompanyName = table.Column<string>(name: "Item Company Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation Date Time", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCompany", x => x.ItemComId);
                });

            migrationBuilder.CreateTable(
                name: "ItemSubCatagory",
                columns: table => new
                {
                    ItemSubCatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCatagoryName = table.Column<string>(name: "Sub Catagory Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ItemMainCatId = table.Column<int>(type: "int", nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSubCatagory", x => x.ItemSubCatId);
                });

            migrationBuilder.CreateTable(
                name: "ItemType",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinancialYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "ItemUnit",
                columns: table => new
                {
                    ItemUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemUnitName = table.Column<string>(name: "Item Unit Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnit", x => x.ItemUnitId);
                });

            migrationBuilder.CreateTable(
                name: "Mast_Comp",
                columns: table => new
                {
                    CompId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(name: "Company Name", type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    OwnerName = table.Column<string>(name: "Owner Name", type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    CompShortName = table.Column<string>(name: "Comp. Short Name", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    State = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MobileNo = table.Column<decimal>(name: "Mobile No", type: "numeric(10,0)", nullable: true),
                    PhoneNo = table.Column<decimal>(name: "Phone No", type: "numeric(10,0)", nullable: true),
                    PINNo = table.Column<decimal>(name: "PIN No", type: "numeric(7,0)", nullable: true),
                    GSTNo = table.Column<string>(name: "GST No", type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    PANNo = table.Column<string>(name: "PAN No", type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    ContactPerson = table.Column<string>(name: "Contact Person", type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mast_Comp", x => x.CompId);
                });

            migrationBuilder.CreateTable(
                name: "Mast_Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(name: "Country Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CountryShortName = table.Column<string>(name: "Country Short Name", type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mast_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "UserMaster",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(name: "User Name", type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    PersonName = table.Column<string>(name: "Person Name", type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    UserType = table.Column<string>(name: "User Type", type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    UpdationDateTime = table.Column<DateTime>(name: "Updation DateTime", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMaster", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse Area Master",
                columns: table => new
                {
                    WarehouseAreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseLocId = table.Column<int>(type: "int", nullable: false),
                    WarehouseAreaName = table.Column<string>(name: "Warehouse Area Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse Area Master", x => x.WarehouseAreaId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse Location Master",
                columns: table => new
                {
                    WarehouseLocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseName = table.Column<string>(name: "Warehouse Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse Location Master", x => x.WarehouseLocId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse Rack Master",
                columns: table => new
                {
                    WarehouseRackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseAreaId = table.Column<int>(type: "int", nullable: false),
                    WarehouseLocId = table.Column<int>(type: "int", nullable: false),
                    WarehouseRackName = table.Column<string>(name: "Warehouse Rack Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse Rack Master", x => x.WarehouseRackId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse Shelf Master",
                columns: table => new
                {
                    WarehouseShelfId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseRackId = table.Column<int>(type: "int", nullable: false),
                    WarehouseAreaId = table.Column<int>(type: "int", nullable: false),
                    WarehouseLocId = table.Column<int>(type: "int", nullable: false),
                    WarehouseShelfName = table.Column<string>(name: "Warehouse Shelf Name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FinanYearId = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(name: "Creation DateTime", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse Shelf Master", x => x.WarehouseShelfId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYear",
                columns: table => new
                {
                    FinanYearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialYearName = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    FinancialYearFrom = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    FinancialYearTo = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    Creation_Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Created_By = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financial Year", x => x.FinanYearId);
                    table.ForeignKey(
                        name: "fk_FinanCompId",
                        column: x => x.CompId,
                        principalTable: "Mast_Comp",
                        principalColumn: "CompId");
                });

            migrationBuilder.CreateTable(
                name: "Mast_Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(name: "Branch Name", type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    BranchShortName = table.Column<string>(name: "Branch Short Name", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OwnerName = table.Column<string>(name: "Owner Name", type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: true),
                    MobileNo = table.Column<decimal>(name: "Mobile No", type: "numeric(10,0)", nullable: true),
                    PhoneNo = table.Column<decimal>(name: "Phone No", type: "numeric(10,0)", nullable: true),
                    PANNo = table.Column<string>(name: "PAN No", type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    GSTNo = table.Column<string>(name: "GST No", type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    PINNo = table.Column<decimal>(name: "PIN No", type: "numeric(7,0)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ContactPerson = table.Column<string>(name: "Contact Person", type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    Creationdate = table.Column<DateTime>(name: "Creation date", type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mast_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "fk_CompId",
                        column: x => x.CompId,
                        principalTable: "Mast_Comp",
                        principalColumn: "CompId");
                });

            migrationBuilder.CreateTable(
                name: "Mast_State",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(name: "State Name", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    StateShortName = table.Column<string>(name: "State Short Name", type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(name: "Creation Datetime", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mast_State", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_Mast_State_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Mast_Country",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "Mast_City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(name: "City Name", type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    CityShortName = table.Column<string>(name: "City Short Name", type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    PinCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CreatedBy = table.Column<int>(name: "Created By", type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(name: "Creation Datetime", type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mast_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Mast_City_StateId",
                        column: x => x.StateId,
                        principalTable: "Mast_State",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYear_CompId",
                table: "FinancialYear",
                column: "CompId");

            migrationBuilder.CreateIndex(
                name: "IX_Mast_Branch_CompId",
                table: "Mast_Branch",
                column: "CompId");

            migrationBuilder.CreateIndex(
                name: "IX_Mast_City_StateId",
                table: "Mast_City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Mast_State_CountryId",
                table: "Mast_State",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialYear");

            migrationBuilder.DropTable(
                name: "Item Catagory");

            migrationBuilder.DropTable(
                name: "ItemCompany");

            migrationBuilder.DropTable(
                name: "ItemSubCatagory");

            migrationBuilder.DropTable(
                name: "ItemType");

            migrationBuilder.DropTable(
                name: "ItemUnit");

            migrationBuilder.DropTable(
                name: "Mast_Branch");

            migrationBuilder.DropTable(
                name: "Mast_City");

            migrationBuilder.DropTable(
                name: "UserMaster");

            migrationBuilder.DropTable(
                name: "Warehouse Area Master");

            migrationBuilder.DropTable(
                name: "Warehouse Location Master");

            migrationBuilder.DropTable(
                name: "Warehouse Rack Master");

            migrationBuilder.DropTable(
                name: "Warehouse Shelf Master");

            migrationBuilder.DropTable(
                name: "Mast_Comp");

            migrationBuilder.DropTable(
                name: "Mast_State");

            migrationBuilder.DropTable(
                name: "Mast_Country");
        }
    }
}
