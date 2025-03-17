using Microsoft.AspNetCore.Mvc;
using InventoryIT.Repository;
using InventoryIT.Models;

namespace InventoryIT.Controllers
{
    public class MastCompController : Controller
    {
        private readonly IMastCompRepository _mastCompRepository;

        public MastCompController(IMastCompRepository mastCompRepository)
        {
            _mastCompRepository = mastCompRepository;
        }
        public IActionResult AddFCompanyMaster()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFCompanyMaster(MastComp mastComp)
        {
            int result = _mastCompRepository.AddFCompanyMaster(mastComp);
            if (result > 0)
            {
                return RedirectToAction("Inventory", "MasterSetup");
            }
            else
            {
                TempData["Failed"] = "Failed to add the company master.";
                return RedirectToAction("AddFCompanyMaster");
            }


        }
        public IActionResult ShowMastCompDataTable()
        {
            return View("mastCompDataTab");
        }
        [HttpGet]
        public IActionResult GetAllCompany()
        {
            try
            {
                var company = _mastCompRepository.GetAllMastcomp();
                if (company == null || !company.Any())
                {
                    return Json(new { data = new List<object>() }); // Return empty data if no suppliers
                }

                var companyData = company.Select(c => new
                {
                    compId = c.CompId,
                    companyName = c.CompanyName,
                    address = c.Address,
                    city = c.City,
                    mobileNo = c.MobileNo
                }).ToList();

                return Json(new { data = companyData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Server Error", message = ex.Message });
            }
        }
    }
}
