using Microsoft.AspNetCore.Mvc;
using InventoryIT.Repository;
using InventoryIT.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class FinancialYearController : Controller
    {
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IMastCompRepository _mastCompRepository;
        public FinancialYearController(IFinancialYearRepository financialYearRepository, IMastCompRepository mastCompRepository)
        {
            _financialYearRepository = financialYearRepository;
            _mastCompRepository = mastCompRepository;
        }
        public IActionResult AddFinancialYear()
        {
            var companies = _mastCompRepository.GetAllMastcomp();
            ViewBag.MastComps = companies.Select(c => new SelectListItem
            {
                Value = c.CompId.ToString(),
                Text = c.CompanyName
            });
            return View();
        }
        [HttpPost]
        public IActionResult AddFinancialYear(FinancialYear financialYear)
        {
            int result = _financialYearRepository.AddFinancialYear(financialYear);
            if (result > 0)
            {
                return RedirectToAction("AddFinancialYear", "FinancialYear");
            }
            else
            {
                TempData["Failed"] = "Failed to add the company master.";
                return RedirectToAction("AddFinancialYear");
            }
        }
    }
}
