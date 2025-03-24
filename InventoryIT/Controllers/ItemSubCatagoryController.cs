using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InventoryIT.Controllers
{
    public class ItemSubCatagoryController : Controller
    {
        private readonly IItemSubCatagoryRepository _itemSubCatagoryRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IItemCatagoryRepository _itemCatagoryRepository;
        public ItemSubCatagoryController(IItemSubCatagoryRepository itemSubCatagoryRepository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository,
            IFinancialYearRepository financialYearRepository, IItemCatagoryRepository itemCatagoryRepository)
        {
            _itemSubCatagoryRepository = itemSubCatagoryRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
            _itemCatagoryRepository = itemCatagoryRepository;
        }
        public IActionResult itemSubCataDataTable()
        {
            return View("ItemSubCataDataTable");
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
            var catagory = _itemCatagoryRepository.GetAllItemCatagory().ToList();
            // If the company, branch, or financial year is not found, return empty data
            if (company == null || branch == null || financialYear == null)
            {
                return Json(new { data = new List<object>() });
            }
            // Call the repository method to get filtered locations directly
            var subCatagories = _itemSubCatagoryRepository.GetFilteredItemSubCat(company.CompId, branch.BranchId, financialYear.FinanYearId)
                .Select(c => new
                {
                    itemSubCatId = c.ItemSubCatId,
                    itemMainCatId = catagory.FirstOrDefault(cat => cat.ItemCatId == c.ItemMainCatId)?.ItemCatagoryName,
                    subCatagoryName = c.SubCatagoryName
                }).ToList();
            return Json(new { data = subCatagories });
        }
        public ActionResult AddItemSubCat()
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
            var catagories = _itemCatagoryRepository.GetAllItemCatagory()
                            .Where(c => c.CompId == company.CompId && c.BranchId == branch.BranchId && c.FinanYearId == financialYear.FinanYearId)
                            .ToList();
            ViewBag.ItemCatagories = catagories.Select(c => new SelectListItem
            {
                Value = c.ItemCatId.ToString(),
                Text = c.ItemCatagoryName
            });
            return View();
        }
        [HttpPost]
        public ActionResult AddItemSubCat(ItemSubCatagory itemSubCatagory)
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
                itemSubCatagory.CompId = company.CompId;
                itemSubCatagory.BranchId = branch.BranchId;
                itemSubCatagory.FinanYearId = financialYear.FinanYearId;
                itemSubCatagory.CreatedBy = userId.Value;

            }
            // Create a new ItemSubCatagory object using the data
            var item = new ItemSubCatagory
            {
                SubCatagoryName = itemSubCatagory.SubCatagoryName,
                ItemMainCatId = itemSubCatagory.ItemMainCatId,
                CompId = itemSubCatagory.CompId,
                BranchId = itemSubCatagory.BranchId,
                FinanYearId = itemSubCatagory.FinanYearId,
                CreatedBy = itemSubCatagory.CreatedBy
            };
            // Save the itemSubCatagory to the database
            _itemSubCatagoryRepository.AddItemSubCat(item);
            TempData["SuccessMessage"] = "ItemSubCatagory details saved successfully.";
            // Redirect to another page or show a success message
            return RedirectToAction("AddItemSubCat", "ItemSubCatagory");
        }
    }
}