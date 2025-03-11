using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class WarehouseRackController : Controller
    {
        private readonly IWarehouseRackRepository _warehouseRackRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IWarehouseLocationRepository _warehouseLocationRepository;
        private readonly IWarehouseAreaRepository _warehouseAreaRepository;
        public WarehouseRackController(IWarehouseRackRepository warehouseRackRepository,
            IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository,
            IFinancialYearRepository financialYearRepository, IWarehouseLocationRepository warehouseLocationRepository, IWarehouseAreaRepository warehouseAreaRepository)
        {
            _warehouseRackRepository = warehouseRackRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
            _warehouseLocationRepository = warehouseLocationRepository;
            _warehouseAreaRepository = warehouseAreaRepository;
        }
        public ActionResult AddWarehouseRack()
        {
            var locations = _warehouseLocationRepository.GetAllWarehouseLocation();
            ViewBag.WarehouseLocationMasters = locations.Select(c => new SelectListItem
            {
                Value = c.WarehouseLocId.ToString(),
                Text = c.WarehouseName
            });
            return PartialView("AddWarehouseRack");
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
        [HttpPost]
        public ActionResult AddWarehouseRack(WarehouseRackMaster warehouseRackMaster)
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
                warehouseRackMaster.CompId = company.CompId;
                warehouseRackMaster.BranchId = branch.BranchId;
                warehouseRackMaster.FinanYearId = financialYear.FinanYearId;
                warehouseRackMaster.CreatedBy = userId.Value;

            }
            // Create a new warehouse rack object using the data
            var rack = new WarehouseRackMaster
            {
                WarehouseLocId = warehouseRackMaster.WarehouseLocId,
                WarehouseAreaId = warehouseRackMaster.WarehouseAreaId,
                WarehouseRackName = warehouseRackMaster.WarehouseRackName,
                CompId = warehouseRackMaster.CompId,
                BranchId = warehouseRackMaster.BranchId,
                FinanYearId = warehouseRackMaster.FinanYearId,
                CreatedBy = warehouseRackMaster.CreatedBy
            };
            // Save warehouse rack details to the database
            _warehouseRackRepository.AddWarehouseRack(rack);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}