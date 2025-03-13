using System.Diagnostics;
using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemTypeController : Controller
    {
        private readonly IItemTypeRepository _typeRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        public ItemTypeController(IItemTypeRepository typeRepository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository)
        {
            _typeRepository = typeRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
        }
        public IActionResult itemtypeDataTable()
        {
            return View("ItemtypeDataTable");
        }
        public ActionResult GetAllData()
        {
            var itemType = _typeRepository.GetAllItemType();
            var masterData = itemType.Select(c => new
            {
                ItemId = c.ItemId,
                ItemName = c.ItemName
            }).ToList();
            return Json(new { data = masterData });
        }
        public ActionResult AddItemType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItemType(ItemType itemType)
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
                itemType.CompId = company.CompId;
                itemType.BranchId = branch.BranchId;
                itemType.FinancialYearId = financialYear.FinanYearId;
                itemType.CreatedBy = userId.Value;

            }
            // Create a new ItemType object using the data
            var item = new ItemType
            {
                ItemName = itemType.ItemName,
                CompId = itemType.CompId,
                BranchId = itemType.BranchId,
                FinancialYearId = itemType.FinancialYearId,
                CreatedBy = itemType.CreatedBy
            };
            // Save the itemType to the database
            _typeRepository.AddItemType(item);
            TempData["SuccessMessage"] = "Item Type details saved successfully.";

            // Redirect to another page or show a success message
            return RedirectToAction("AddItemType", "ItemType");
        }
    }
}