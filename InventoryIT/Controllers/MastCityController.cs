using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class MastCityController : Controller
    {
        private readonly IMastCityRepository _mastCityRepository;
        private readonly IMastStateRepository _mastStateRepository;
        public MastCityController(IMastCityRepository mastCityRepository, IMastStateRepository mastStateRepository)
        {
            _mastCityRepository = mastCityRepository;
            _mastStateRepository = mastStateRepository;
        }
        public ActionResult CityDataTab()
        {
            return View("cityDataTab");
        }
        public ActionResult GetCity()
        {
            var city = _mastCityRepository.GetAllMastCity().Select(c => new
            {
                cityId = c.CityId,
                cityName =  c.CityName,
                cityShortName = c.CityShortName
            }).ToList();
            return Json(new { data = city });
        }
        public IActionResult AddMastCity()
        {
            var state = _mastStateRepository.GetAllState();
            ViewBag.MastStates = state.Select(c => new SelectListItem
            {
                Value = c.StateId.ToString(),
                Text = c.StateName
            });
            return View();
        }
        [HttpPost]
        public IActionResult AddMastCity(MastCity mastCity)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                mastCity.CreatedBy = userId.Value;
            }
            var city = new MastCity
            {
                CityName = mastCity.CityName,
                CityShortName = mastCity.CityShortName,
                StateId = mastCity.StateId,
                PinCode = mastCity.PinCode,
                CreatedBy = mastCity.CreatedBy
            };
            // Save the city details to the database
            _mastCityRepository.AddMastCity(city);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}
