using Microsoft.AspNetCore.Mvc;
using InventoryIT.Repository;
using InventoryIT.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class MastCompController : Controller
    {
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IMastStateRepository _mastStateRepository;
        private readonly IMastCityRepository _mastCityRepository;
        public MastCompController(IMastCompRepository mastCompRepository,
            IMastStateRepository mastStateRepository, IMastCityRepository mastCityRepository)
        {
            _mastCompRepository = mastCompRepository;
            _mastStateRepository = mastStateRepository;
            _mastCityRepository = mastCityRepository;
        }
        public IActionResult AddFCompanyMaster()
        {
            ViewBag.MastStates = _mastStateRepository.GetAllState().Select(c => new SelectListItem
            {
                Value = c.StateId.ToString(),
                Text = c.StateName
            });
            return View();
        }
        // Action to fetch city by selected state
        [HttpGet]
        public JsonResult GetCityByState(int stateid)
        {
            var city = _mastCityRepository.GetAllMastCity()
                                            .Where(a => a.StateId == stateid)
                                            .Select(c => new SelectListItem
                                            {
                                                Value = c.CityId.ToString(),
                                                Text = c.CityName
                                            }).ToList();
            return Json(city); // Return city as JSON
        }
        [HttpPost]
        public IActionResult AddFCompanyMaster(MastComp mastComp)
        {
            int result = _mastCompRepository.AddFCompanyMaster(mastComp);
            if (result > 0)
            {
                return RedirectToAction("AddFCompanyMaster", "MastComp");
            }
            else
            {
                TempData["Failed"] = "Failed to add the company master.";
                return RedirectToAction("AddFCompanyMaster", "MastComp");
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
                var cities = _mastCityRepository.GetAllMastCity().ToList();
                if (company == null || !company.Any())
                {
                    return Json(new { data = new List<object>() }); // Return empty data if no suppliers
                }
                var companyData = company.Select(c => new
                {
                    CompId = c.CompId,
                    CompanyName = c.CompanyName,
                    Address = c.Address,
                    City = cities.FirstOrDefault(city => city.CityId == c.City)?.CityName, 
                    MobileNo = c.MobileNo
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
