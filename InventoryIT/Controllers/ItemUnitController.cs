using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemUnitController : Controller
    {
        private readonly IItemUnitRepository _itemUnitRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        public ItemUnitController(IItemUnitRepository itemUnitRepository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository)
        {
            _itemUnitRepository = itemUnitRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
        }
        public IActionResult itemUnitDataTable()
        {
            return View("ItemUnitDataTable");
        }
        public ActionResult GetAllUnit()
        {
            var itemunit = _itemUnitRepository.GetAllItemUnit();
            var masterData = itemunit.Select(c => new
            {
                ItemUnitId = c.ItemUnitId,
                ItemUnitName = c.ItemUnitName
            }).ToList();
            return Json(new { data = masterData });
        }
        public ActionResult AddItemUnit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItemUnit(ItemUnit itemUnit)
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
                itemUnit.CompId = company.CompId;
                itemUnit.BranchId = branch.BranchId;
                itemUnit.FinanYearId = financialYear.FinanYearId;
                itemUnit.CreatedBy = userId.Value;

            }
            // Create a new ItemUnit object using the data
            var item = new ItemUnit
            {
                ItemUnitName = itemUnit.ItemUnitName,
                CompId = itemUnit.CompId,
                BranchId = itemUnit.BranchId,
                FinanYearId = itemUnit.FinanYearId,
                CreatedBy = itemUnit.CreatedBy
            };
            // Save the itemUnit to the database
            _itemUnitRepository.AddItemUnit(item);
            TempData["SuccessMessage"] = "Item Unit details saved successfully.";

            // Redirect to another page or show a success message
            return RedirectToAction("AddItemUnit", "ItemUnit");
        }
    }
}