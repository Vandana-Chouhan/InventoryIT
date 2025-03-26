using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class WarehouseLocationController : Controller
    {
        private readonly IWarehouseLocationRepository _warehouseLocationRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        public WarehouseLocationController(IWarehouseLocationRepository warehouseLocationRepository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository)
        {
            _warehouseLocationRepository = warehouseLocationRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
        }
        public ActionResult warehouseLocaDataTab()
        {
            return View("WarehouseLocaDataTab");
        }
        public ActionResult GetLocation()
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
            var locations = _warehouseLocationRepository.GetFilteredWarehouseLocations(company.CompId, branch.BranchId, financialYear.FinanYearId)
                .Select(c => new
                {
                    warehouseLocId = c.WarehouseLocId,
                    warehouseName = c.WarehouseName
                })
                .ToList();

            // Return the filtered data
            return Json(new { data = locations });
        }
        public IActionResult AddWarehouselocation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWarehouselocation(WarehouseLocationMaster warehouseLocationMaster)
        {
            if (!ModelState.IsValid)
            {
                return View(warehouseLocationMaster); // Return same view with validation errors
            }

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
                    warehouseLocationMaster.CompId = company.CompId;
                    warehouseLocationMaster.BranchId = branch.BranchId;
                    warehouseLocationMaster.FinanYearId = financialYear.FinanYearId;
                    warehouseLocationMaster.CreatedBy = userId.Value;

                }
                // Create a new Warehouselocation object using the data
                var location = new WarehouseLocationMaster
                {
                    WarehouseName = warehouseLocationMaster.WarehouseName,
                    CompId = warehouseLocationMaster.CompId,
                    BranchId = warehouseLocationMaster.BranchId,
                    FinanYearId = warehouseLocationMaster.FinanYearId,
                    CreatedBy = warehouseLocationMaster.CreatedBy
                };
                // Save the Warehouselocation details to the database
                _warehouseLocationRepository.AddWarehouselocation(location);

                TempData["SuccessMessage"] = "Warehouse Location details saved successfully.";

                // Redirect to another page or show a success message
                return RedirectToAction("AddWarehouselocation", "WarehouseLocation");
            }
        }
    }

