using System.Reflection.Metadata.Ecma335;
using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class MastStateController : Controller
    {
        private readonly IMastStateRepository _mastStateRepository;
        private readonly IMastCountryRepository _mastCountryRepository;
        public MastStateController(IMastStateRepository mastStateRepository, IMastCountryRepository mastCountryRepository)
        {
            _mastStateRepository = mastStateRepository;
            _mastCountryRepository = mastCountryRepository;
        }

        public IActionResult AddState()
        {
            var country = _mastCountryRepository.GetAllCountry();
            ViewBag.MastCountries = country.Select(c => new SelectListItem
            {
                Value = c.CountryId.ToString(),
                Text = c.CountryName
            });
            return View();
        }
        [HttpPost]
        public IActionResult AddState(MastState mastState)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                mastState.CreatedBy = userId.Value;
            }
            var state = new MastState
            {
                StateName = mastState.StateName,
                StateShortName = mastState.StateShortName,
                CountryId = mastState.CountryId,
                CreatedBy = mastState.CreatedBy
            };
            // Save the State details to the database
            _mastStateRepository.AddState(state);
            // Redirect to another page or show a success message
            return RedirectToAction("Inventory", "MasterSetup");
        }
    }
}
