using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class MastCountryController : Controller
    {
        private readonly IMastCountryRepository _mastCountryRepository;
        public MastCountryController(IMastCountryRepository mastCountryRepository)
        {
            _mastCountryRepository = mastCountryRepository;
        }
        public IActionResult AddCountry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCountry(MastCountry mastCountry)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                mastCountry.CreatedBy = userId.Value;
            }
            var countryname = new MastCountry
            {
                CountryName = mastCountry.CountryName,
                CountryShortName = mastCountry.CountryShortName,
                CreatedBy = mastCountry.CreatedBy
            };
            // Save the country details to the database
            _mastCountryRepository.AddCountry(countryname);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}
