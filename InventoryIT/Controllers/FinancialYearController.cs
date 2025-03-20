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
        public ActionResult AddFinancialYear()
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
        public ActionResult AddFinancialYear(FinancialYear financialYear)
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
        public IActionResult ShowFinanYearDataTable()
        {
            return View("mastFinancialYearDataTab");
        }
        [HttpGet]
        public IActionResult GetAllYear()
        {
            try
            {
                var year = _financialYearRepository.GetAllFinancialYear();
                var company = _mastCompRepository.GetAllMastcomp().ToList();
                var yearData = year.Select(c => new
                {
                    finanYearId = c.FinanYearId,
                    financialYearFrom = c.FinancialYearFrom,
                    financialYearTo = c.FinancialYearTo,
                    financialYear = c.FinancialYearName,
                    CompanyName = company.FirstOrDefault(comp => comp.CompId == c.CompId)?.CompanyName,
                }).ToList();
                return Json(new { data = yearData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Server Error", message = ex.Message });
            }
        }
    }
}
