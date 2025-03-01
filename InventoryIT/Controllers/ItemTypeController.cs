using System.Diagnostics;
using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemTypeController : Controller
    {
        private readonly ItemTypeRepository _itemTypeRepository;
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

            // Check if session data exists, otherwise redirect to error page
            if (string.IsNullOrEmpty(compName) || string.IsNullOrEmpty(branchName) || string.IsNullOrEmpty(finanYearName))
            {
                return RedirectToAction("ErrorPage");
            }

            // Example: Fetch corresponding IDs based on names from your database or repository
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            var financialYear = _financialYearRepository.GetAllFinancialYear().FirstOrDefault(fy => fy.FinancialYearName == finanYearName);

            if (company != null && branch != null && financialYear != null)
            {
                itemType.CompId = company.CompId;
                itemType.BranchId = branch.BranchId;
                itemType.FinancialYearId = financialYear.FinanYearId;
            }

            // Create a new ItemType object using the data
            var item = new ItemType
            {
                ItemName = itemType.ItemName,
                CompId = itemType.CompId,
                BranchId = itemType.BranchId,
                FinancialYearId = itemType.FinancialYearId
                //CreatedBy = 1 
            };

            // Save the itemType to the database
            _typeRepository.AddItemType(item);

            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }

    }
}

