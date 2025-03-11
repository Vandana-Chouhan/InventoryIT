using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryIT.ViewModels;


namespace InventoryIT.Controllers
{
    public class ItemMasterController : Controller
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IMastItemSupplierRateRepository _mastItemSupplierRateRepository;
        private readonly IMastItemStkRepository _mastItemStkRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IWarehouseLocationRepository _warehouseLocationRepository;
        private readonly IWarehouseAreaRepository _warehouseAreaRepository;
        private readonly IWarehouseRackRepository _warehouseRackRepository;
        private readonly IWarehouseShelfRepository _warehouseShelfRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IItemUnit1Repository _itemUnit1Repository;
        private readonly IItemCompanytRepository _itemCompanytRepository;
        private readonly IItemCatagoryRepository _itemCatagoryRepository;
        private readonly IItemSubCatagoryRepository _itemSubCatagoryRepository;
        private readonly ISupplierMasterRepository _supplierMasterRepository;
        public ItemMasterController(IItemMasterRepository itemMasterRepository,
                   IMastItemSupplierRateRepository mastItemSupplierRateRepository,
                   IMastItemStkRepository mastItemStkRepository,
                   IItemTypeRepository itemTypeRepository,
                   IItemUnit1Repository itemUnit1Repository,
                   IItemCompanytRepository itemCompanytRepository,
                   IItemCatagoryRepository itemCatagoryRepository,
                   IItemSubCatagoryRepository itemSubCatagoryRepository,
                   IWarehouseShelfRepository warehouseShelfRepository,
                   IWarehouseRackRepository warehouseRackRepository,
                   IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository,
                   IFinancialYearRepository financialYearRepository,
                   IWarehouseLocationRepository warehouseLocationRepository,
                   IWarehouseAreaRepository warehouseAreaRepository, ISupplierMasterRepository supplierMasterRepository)
        {
            _mastItemSupplierRateRepository = mastItemSupplierRateRepository;
            _mastItemStkRepository = mastItemStkRepository;
            _itemMasterRepository = itemMasterRepository;
            _warehouseShelfRepository = warehouseShelfRepository;
            _warehouseRackRepository = warehouseRackRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
            _warehouseLocationRepository = warehouseLocationRepository;
            _warehouseAreaRepository = warehouseAreaRepository;
            _itemTypeRepository = itemTypeRepository;
            _itemUnit1Repository = itemUnit1Repository;
            _itemCompanytRepository = itemCompanytRepository;
            _itemCatagoryRepository = itemCatagoryRepository;
            _itemSubCatagoryRepository = itemSubCatagoryRepository;
            _supplierMasterRepository = supplierMasterRepository;
        }
        public ActionResult AddItemMaster()
        {
            var locations = _warehouseLocationRepository.GetAllWarehouseLocation();
            var itemTypes = _itemTypeRepository.GetAllItemType();
            var itemUnit1 = _itemUnit1Repository.GetAllItemUnit1();
            var itemCompanies = _itemCompanytRepository.GetAllItemCompany();
            var itemCategories = _itemCatagoryRepository.GetAllItemCatagory();
            var suppliers = _supplierMasterRepository.GetAllSupplier();

            ViewBag.SupplierMasters = suppliers.Select(c => new SelectListItem
            {
                Value = c.SuppId.ToString(),
                Text = c.SupplierName
            });
            // Pass data to ViewBag for rendering in the view
            ViewBag.WarehouseLocationMasters = locations.Select(c => new SelectListItem
            {
                Value = c.WarehouseLocId.ToString(),
                Text = c.WarehouseName
            });
            // Pass other dropdown data for item types, units, categories, subcategories
            ViewBag.ItemTypes = itemTypes.Select(c => new SelectListItem
            {
                Value = c.ItemId.ToString(),
                Text = c.ItemName
            });
            ViewBag.ItemUnits = itemUnit1.Select(c => new SelectListItem
            {
                Value = c.ItemUnitId1.ToString(),
                Text = c.ItemUnitName
            });
            ViewBag.ItemCompanies = itemCompanies.Select(c => new SelectListItem
            {
                Value = c.ItemComId.ToString(),
                Text = c.ItemCompanyName
            });

            ViewBag.ItemCatagories = itemCategories.Select(c => new SelectListItem
            {
                Value = c.ItemCatId.ToString(),
                Text = c.ItemCatagoryName
            });
            return PartialView("AddItemMaster");
        }
        //Action to fetch Subcatagory by selected Catagory
        [HttpGet]
        public JsonResult GetSubCatagoryByCatagory(int catagoryId)
        {
            var subcatagory = _itemSubCatagoryRepository.GetAllItemSubCat()
                        .Where(a => a.ItemMainCatId == catagoryId)
                        .Select(c => new SelectListItem
                        {
                            Value = c.ItemSubCatId.ToString(),
                            Text = c.SubCatagoryName
                        }).ToList();
            return Json(subcatagory); // Return subcatagory as JSON
        }
        // Action to fetch areas by selected location
        [HttpGet]
        public JsonResult GetAreasByLocation(int locationId)
        {
            var areas = _warehouseAreaRepository.GetAllWarehouseArea()
                        .Where(a => a.WarehouseLocId == locationId)
                        .Select(c => new SelectListItem
                        {
                            Value = c.WarehouseAreaId.ToString(),
                            Text = c.WarehouseAreaName
                        }).ToList();
            return Json(areas); // Return areas as JSON
        }
        //Action to fetch racks by selected Areas
        [HttpGet]
        public JsonResult GetRackByAreas(int areaId)
        {
            var racks = _warehouseRackRepository.GetAllWarehouseRack()
                        .Where(a => a.WarehouseAreaId == areaId)
                        .Select(c => new SelectListItem
                        {
                            Value = c.WarehouseRackId.ToString(),
                            Text = c.WarehouseRackName
                        }).ToList();
            return Json(racks); // Return racks as JSON
        }
        [HttpGet]
        public JsonResult GetShelvesByRack(int rackId)
        {
            var shelves = _warehouseShelfRepository.GetAllWarehouseShelf()
                        .Where(a => a.WarehouseRackId == rackId)
                        .Select(c => new SelectListItem
                        {
                            Value = c.WarehouseShelfId.ToString(),
                            Text = c.WarehouseShelfName
                        }).ToList();
            return Json(shelves); // Return shelves as JSON
        }
        [HttpPost]
        public ActionResult AddItemMaster(ItemMasterViewModel itemMasterViewModel)
        {
            // Retrieve session values as strings
            string? compName = HttpContext.Session.GetString("CompanyName");
            string? branchName = HttpContext.Session.GetString("BranchName");
            string? finanYearName = HttpContext.Session.GetString("FinancialYear");

            int? userId = HttpContext.Session.GetInt32("UserId");

            // Check if session data exists, otherwise redirect to error page
            if (string.IsNullOrEmpty(compName) || string.IsNullOrEmpty(branchName) || string.IsNullOrEmpty(finanYearName) || userId == null)
            {
                return RedirectToAction("ErrorPage");
            }
            // Example: Fetch corresponding IDs based on names from database or repository
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            var financialYear = _financialYearRepository.GetAllFinancialYear().FirstOrDefault(fy => fy.FinancialYearName == finanYearName);
            if (company != null && branch != null && financialYear != null)
            {
                itemMasterViewModel.itemMaster.CompId = company.CompId;
                itemMasterViewModel.itemMaster.BranchId = branch.BranchId;
                itemMasterViewModel.itemMaster.FinanYearId = financialYear.FinanYearId;
                itemMasterViewModel.itemMaster.CreatedBy = userId.Value;
            }
            // Create a new item master object using the data
            var itemmast = new ItemMaster
            {
                ItemName = itemMasterViewModel.itemMaster.ItemName,
                ItemType = itemMasterViewModel.itemMaster.ItemType,
                ItemCode = itemMasterViewModel.itemMaster.ItemCode,
                ItemCompany = itemMasterViewModel.itemMaster.ItemCompany,
                ItemCatagory = itemMasterViewModel.itemMaster.ItemCatagory,
                ItemSubCatagory = itemMasterViewModel.itemMaster.ItemSubCatagory,
                ItemUnit1 = itemMasterViewModel.itemMaster.ItemUnit1,
                ItemUnit2 = itemMasterViewModel.itemMaster.ItemUnit2,
                WarehouseLocation = itemMasterViewModel.itemMaster.WarehouseLocation,
                WarehouseArea = itemMasterViewModel.itemMaster.WarehouseArea,
                WarehouseRack = itemMasterViewModel.itemMaster.WarehouseRack,
                WarehouseShelf = itemMasterViewModel.itemMaster.WarehouseShelf,
                PartNo = itemMasterViewModel.itemMaster.PartNo,
                CompId = itemMasterViewModel.itemMaster.CompId,
                BranchId = itemMasterViewModel.itemMaster.BranchId,
                FinanYearId = itemMasterViewModel.itemMaster.FinanYearId,
                CreatedBy = itemMasterViewModel.itemMaster.CreatedBy
            };
            // Save item master details to the database
            _itemMasterRepository.AddItemMaster(itemmast);

            //  int itemId = itemMasterViewModel.itemMaster.ItemId;
            _itemMasterRepository.Save();

            int itemId = itemmast.ItemId;

            // Insert data into MastItemSupplierRate table using the ItemId
            var supplierRate = new MastItemSupplierRate
            {
                ItemId = itemId,
                SupplierRate = itemMasterViewModel.supplierRate.SupplierRate,
                SupplierName = itemMasterViewModel.supplierRate.SupplierName,
                SupplierCompanyName = itemMasterViewModel.supplierRate.SupplierCompanyName,
                CompId = itemMasterViewModel.supplierRate.CompId,
                BranchId = itemMasterViewModel.supplierRate.BranchId,
                FinanYearId = itemMasterViewModel.supplierRate.FinanYearId,
                CreatedBy = userId.Value
            };
            _mastItemSupplierRateRepository.AddItemSupplierRate(supplierRate);
            _mastItemSupplierRateRepository.Save();

            // Insert data into MastItemStk table using the ItemId
            var itemStk = new MastItemStk
            {
                ItemId = itemId,
                OpeningQuantity = itemMasterViewModel.mastItemStk.OpeningQuantity,
                CurrentQuantity = itemMasterViewModel.mastItemStk.OpeningQuantity,
                ClosingQuantity = itemMasterViewModel.mastItemStk.ClosingQuantity,
                OpeningValue = itemMasterViewModel.mastItemStk.OpeningValue,
                Gst = itemMasterViewModel.mastItemStk.Gst,
                PurchaseRate = itemMasterViewModel.mastItemStk.PurchaseRate,
                SalesRate = itemMasterViewModel.mastItemStk.SalesRate,
                BufferStock = itemMasterViewModel.mastItemStk.BufferStock,
                CompId = itemMasterViewModel.itemMaster.CompId,
                BranchId = itemMasterViewModel.itemMaster.BranchId,
                FinanYearId = itemMasterViewModel.itemMaster.FinanYearId,
                CreatedBy = userId.Value
            };
            _mastItemStkRepository.AddItemStk(itemStk);
            _mastItemStkRepository.Save();
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}