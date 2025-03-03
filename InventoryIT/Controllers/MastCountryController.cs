using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class MastCountryController : Controller
    {
        public IActionResult AddCountry()
        {
            return View();
        }
    }
}
