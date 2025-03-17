using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class WarehouseShelfController : Controller
    {
        private readonly IWarehouseRackRepository _warehouseRackRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IWarehouseLocationRepository _warehouseLocationRepository;
        private readonly IWarehouseAreaRepository _warehouseAreaRepository;
        private readonly IWarehouseShelfRepository _warehouseShelfRepository;
        public WarehouseShelfController(IWarehouseShelfRepository warehouseShelfRepository, IWarehouseRackRepository warehouseRackRepository,
            IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository,
            IFinancialYearRepository financialYearRepository, IWarehouseLocationRepository warehouseLocationRepository,
            IWarehouseAreaRepository warehouseAreaRepository)
        {
            _warehouseShelfRepository = warehouseShelfRepository;
            _warehouseRackRepository = warehouseRackRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
            _warehouseLocationRepository = warehouseLocationRepository;
            _warehouseAreaRepository = warehouseAreaRepository;
        }
        public ActionResult warehouseShelfDataTab()
        {
            return View("WarehouseShelfDataTab");
        }
        public ActionResult GetShelf()
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
            // Call the repository method to get filtered shelves directly
            var shelves = _warehouseShelfRepository.GetFilteredWarehouseShelf(company.CompId, branch.BranchId, financialYear.FinanYearId)
                .Select(s => new
                {
                    WarehouseShelfId = s.WarehouseShelfId,
                    WarehouseShelfName = s.WarehouseShelfName
                }).ToList();
            return Json(new { data = shelves });
        }
        public ActionResult AddWarehouseShelf()
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
        [HttpPost]
        public ActionResult AddWarehouseShelf(WarehouseShelfMaster warehouseShelfMaster)
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
                warehouseShelfMaster.CompId = company.CompId;
                warehouseShelfMaster.BranchId = branch.BranchId;
                warehouseShelfMaster.FinanYearId = financialYear.FinanYearId;
                warehouseShelfMaster.CreatedBy = userId.Value;

            }
            // Create a new warehouse shelf object using the data
            var shelf = new WarehouseShelfMaster
            {
                WarehouseLocId = warehouseShelfMaster.WarehouseLocId,
                WarehouseAreaId = warehouseShelfMaster.WarehouseAreaId,
                WarehouseRackId = warehouseShelfMaster.WarehouseRackId,
                WarehouseShelfName = warehouseShelfMaster.WarehouseShelfName,
                CompId = warehouseShelfMaster.CompId,
                BranchId = warehouseShelfMaster.BranchId,
                FinanYearId = warehouseShelfMaster.FinanYearId,
                CreatedBy = warehouseShelfMaster.CreatedBy
            };
            // Save warehouse shelf details to the database
            _warehouseShelfRepository.AddWarehouseShelf(shelf);
            TempData["SuccessMessage"] = "Warehouse Shelf details saved successfully.";

            // Redirect to another page or show a success message
            return RedirectToAction("AddWarehouseShelf", "WarehouseShelf");
        }
    }
}