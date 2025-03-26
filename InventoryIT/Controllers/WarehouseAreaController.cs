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
        public ActionResult warehouseAreaDataTab()
        {
            return View("WarehouseAreaDataTab");
        }
        public ActionResult GetArea()
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
            var location = _warehouseLocationRepository.GetAllWarehouseLocation().ToList();

            // If the company, branch, or financial year is not found, return empty data
            if (company == null || branch == null || financialYear == null)
            {
                return Json(new { data = new List<object>() });
            }
            // Call the repository method to get filtered locations directly
            var areas = _warehouseAreaRepository.GetFilteredWarehouseArea(company.CompId, branch.BranchId, financialYear.FinanYearId)
                .Select(c => new
                {
                    warehouseLocId = location.FirstOrDefault(loc => loc.WarehouseLocId == c.WarehouseLocId)?.WarehouseName,
                    warehouseAreaId = c.WarehouseAreaId,
                    warehouseAreaName = c.WarehouseAreaName
                })
                .ToList();
            return Json(new { data = areas });
        }
        public ActionResult AddWarehouseArea()
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

            TempData["SuccessMessage"] = "Warehouse Area details saved successfully.";

            // Redirect to another page or show a success message
            return RedirectToAction("AddWarehouseArea", "WarehouseArea");
        }
        public IActionResult ShowWarehousecards()
        {
            return View("WarehouseCards");
        }
    }
}
