using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemUnit1Controller : Controller
    {
        private readonly IItemUnit1Repository _itemUnit1Repository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        public ItemUnit1Controller(IItemUnit1Repository itemUnit1Repository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository)
        {
            _itemUnit1Repository = itemUnit1Repository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
        }
        public ActionResult AddItemUnit1()
        {
            return PartialView("AddItemUnit1");
        }
        [HttpPost]
        public ActionResult AddItemUnit1(ItemUnit1 itemUnit)
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
            var item = new ItemUnit1
            {
                ItemUnitName = itemUnit.ItemUnitName,
                CompId = itemUnit.CompId,
                BranchId = itemUnit.BranchId,
                FinanYearId = itemUnit.FinanYearId,
                CreatedBy = itemUnit.CreatedBy
            };
            // Save the itemUnit to the database
            _itemUnit1Repository.AddItemUnit1(item);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}

