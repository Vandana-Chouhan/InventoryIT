using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemCompanyController : Controller
    {
        private readonly IItemCompanytRepository _itemCompanytRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        public ItemCompanyController(IItemCompanytRepository itemCompanytRepository, IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository)
        {
            _itemCompanytRepository = itemCompanytRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
        }
        public ActionResult AddItemCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItemCompany(ItemCompany itemCompany)
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
                itemCompany.CompId = company.CompId;
                itemCompany.BranchId = branch.BranchId;
                itemCompany.FinanYearId = financialYear.FinanYearId;
                itemCompany.CreatedBy = userId.Value;

            }
            // Create a new ItemCompany object using the data
            var item = new ItemCompany
            {
                ItemCompanyName = itemCompany.ItemCompanyName,
                CompId = itemCompany.CompId,
                BranchId = itemCompany.BranchId,
                FinanYearId = itemCompany.FinanYearId,
                CreatedBy = itemCompany.CreatedBy
            };
            // Save the itemCompany to the database
            _itemCompanytRepository.AddItemCompany(item);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}

