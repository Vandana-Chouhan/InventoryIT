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
        public ActionResult warehouseRackDataTab()
        {
            return View("WarehouseRackDataTab");
        }
        public ActionResult GetRacks()
        {
            // Retrieve session values
            string? compName = HttpContext.Session.GetString("CompanyName");
            string? branchName = HttpContext.Session.GetString("BranchName");
            string? finanYearName = HttpContext.Session.GetString("FinancialYear");

            if (string.IsNullOrEmpty(compName) || string.IsNullOrEmpty(branchName) || string.IsNullOrEmpty(finanYearName))
            {
                return Json(new { data = new List<object>() });
            }
            // Fetch company, branch, and financial year IDs based on session values
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            var financialYear = _financialYearRepository.GetAllFinancialYear().FirstOrDefault(fy => fy.FinancialYearName == finanYearName);

            // If the company, branch, or financial year is not found, return empty data
            if (company == null || branch == null || financialYear == null)
            {
                return Json(new { data = new List<object>() });
            }
            // Call the repository method to get filtered locations directly
            var racks = _warehouseRackRepository.GetFilteredWarehouseRack(company.CompId, branch.BranchId, financialYear.FinanYearId)
                .Select(r => new
                {
                    WarehouseRackId = r.WarehouseRackId,
                    WarehouseAreaId = r.WarehouseAreaId,
                    WarehouseLocId = r.WarehouseLocId,
                    WarehouseRackName = r.WarehouseRackName
                })
                .ToList();
            return Json(new { data = racks });
        }
        public ActionResult AddWarehouseRack()
        {
            string? compName = HttpContext.Session.GetString("CompanyName");
            string? branchName = HttpContext.Session.GetString("BranchName");
            string? finanYearName = HttpContext.Session.GetString("FinancialYear");

            if (string.IsNullOrEmpty(compName) || string.IsNullOrEmpty(branchName) || string.IsNullOrEmpty(finanYearName))
            {
                return Json(new { data = new List<object>() });
            }
            // Fetch company, branch, and financial year IDs based on session values
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            var financialYear = _financialYearRepository.GetAllFinancialYear().FirstOrDefault(fy => fy.FinancialYearName == finanYearName);

            // If the company, branch, or financial year is not found, return empty data
            if (company == null || branch == null || financialYear == null)
            {
                return Json(new { data = new List<object>() });
            }
            // Call the repository method to get filtered locations directly
            var locations = _warehouseLocationRepository.GetAllWarehouseLocation()
                            .Where(c => c.CompId == company.CompId && c.BranchId == branch.BranchId && c.FinanYearId == financialYear.FinanYearId)
                            .ToList();
            ViewBag.WarehouseLocationMasters = locations.Select(c => new SelectListItem
            {
                Value = c.WarehouseLocId.ToString(),
                Text = c.WarehouseName
            });
            return View();
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
            TempData["SuccessMessage"] = "Warehouse Rack details saved successfully.";

            // Redirect to another page or show a success message
            return RedirectToAction("AddWarehouseRack", "WarehouseRack");
        }
    }
}