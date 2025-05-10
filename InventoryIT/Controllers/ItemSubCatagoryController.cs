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
            var subCatagories= _itemSubCatagoryRepository.GetAllItemSubCat();
            var masterData = subCatagories.Select(c => new
            {
                ItemSubCatId = c.ItemSubCatId,
                ItemMainCatId= c.ItemMainCatId,
                SubCatagoryName = c.SubCatagoryName
            }).ToList();
            return Json(new { data = masterData });
        }
        public ActionResult AddItemSubCat()
        {
            var catagories = _itemCatagoryRepository.GetAllItemCatagory();
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