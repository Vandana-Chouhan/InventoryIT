using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemCatagoryController : Controller
    {
        private readonly IItemCatagoryRepository _itemCatagoryRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;

        public ItemCatagoryController(IItemCatagoryRepository itemCatagoryRepository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository)
        {
            _itemCatagoryRepository = itemCatagoryRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
        }
        public IActionResult itemcategoryDataTable()
        {
            return View("ItemCatagoryDataTable");
        }
        public ActionResult GetAllData()
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
            var catagory = _itemCatagoryRepository.GetFilteredItemCatagory(company.CompId, branch.BranchId, financialYear.FinanYearId)
                .Select(c => new
                {
                    ItemCatId = c.ItemCatId,
                    ItemCatagoryName = c.ItemCatagoryName
                }).ToList();
            return Json(new { data = catagory });
        }
        public ActionResult AddItemCatagory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItemCatagory(ItemCatagory itemCatagory)
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
            if (!ModelState.IsValid)
            {
                return View(itemCatagory); // Return with validation messages
            }
            // Example: Fetch corresponding IDs based on names from database or repository
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            var financialYear = _financialYearRepository.GetAllFinancialYear().FirstOrDefault(fy => fy.FinancialYearName == finanYearName);
            if (company == null && branch == null && financialYear == null)
            {
                ModelState.AddModelError("", "Invalid company, branch, or financial year.");
                return View(itemCatagory); ;
            }
            itemCatagory.CompId = company.CompId;
            itemCatagory.BranchId = branch.BranchId;
            itemCatagory.FinanYearId = financialYear.FinanYearId;
            itemCatagory.CreatedBy = userId.Value;

            // 🔹 Save to the database
            _itemCatagoryRepository.AddItemCatagory(itemCatagory);
            TempData["SuccessMessage"] = "Item Catagory details saved successfully.";

            // Redirect to another page or show a success message
            return RedirectToAction("AddItemCatagory", "ItemCatagory");
        }
    }
}