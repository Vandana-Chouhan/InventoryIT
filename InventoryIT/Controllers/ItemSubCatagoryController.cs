using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemSubCatagoryController : Controller
    {
        private readonly ItemSubCatagoryController _itemSubCatagoryRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IUserMasterRepository _userMasterRepository;
        public ItemSubCatagoryController(ItemSubCatagoryController itemSubCatagoryController, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository, IUserMasterRepository userMasterRepository)
        {
            _itemSubCatagoryRepository = itemSubCatagoryController;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
            _userMasterRepository = userMasterRepository;
        }
        public ActionResult AddItemSubCat()
        {
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
                CompId = itemSubCatagory.CompId,
                BranchId = itemSubCatagory.BranchId,
                FinanYearId = itemSubCatagory.FinanYearId,
                CreatedBy = itemSubCatagory.CreatedBy
            };
            // Save the itemSubCatagory to the database
            _itemSubCatagoryRepository.AddItemSubCat(item);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}

