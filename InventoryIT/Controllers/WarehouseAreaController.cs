using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class WarehouseAreaController : Controller
    {
        private readonly IWarehouseAreaRepository _warehouseAreaRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IWarehouseLocationRepository _warehouseLocationRepository;
        public WarehouseAreaController(IWarehouseAreaRepository warehouseAreaRepository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository,
            IFinancialYearRepository financialYearRepository, IWarehouseLocationRepository warehouseLocationRepository)
        {
            _warehouseAreaRepository = warehouseAreaRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
            _warehouseLocationRepository = warehouseLocationRepository;
        }
        public ActionResult AddWarehouseArea()
        {
            var locations = _warehouseLocationRepository.GetAllWarehouseLocation();
            ViewBag.WarehouseLocationMasters = locations.Select(c => new SelectListItem
            {
                Value = c.WarehouseLocId.ToString(),
                Text = c.WarehouseName
            });
            return View();
        }
        [HttpPost]
        public ActionResult AddWarehouseArea(WarehouseAreaMaster warehouseAreaMaster)
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
                warehouseAreaMaster.CompId = company.CompId;
                warehouseAreaMaster.BranchId = branch.BranchId;
                warehouseAreaMaster.FinanYearId = financialYear.FinanYearId;
                warehouseAreaMaster.CreatedBy = userId.Value;

            }
            // Create a new ItemSubCatagory object using the data
            var area = new WarehouseAreaMaster
            {
                WarehouseAreaName = warehouseAreaMaster.WarehouseAreaName,
                WarehouseLocId = warehouseAreaMaster.WarehouseLocId,
                CompId = warehouseAreaMaster.CompId,
                BranchId = warehouseAreaMaster.BranchId,
                FinanYearId = warehouseAreaMaster.FinanYearId,
                CreatedBy = warehouseAreaMaster.CreatedBy
            };
            // Save the warehousearea details to the database
            _warehouseAreaRepository.AddWarehouseArea(area);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}
