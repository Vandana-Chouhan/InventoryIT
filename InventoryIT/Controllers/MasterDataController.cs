using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class MasterDataController : Controller
    {
        public IActionResult Index()
        {
            return View("_MasterDataCardsPartial");
        }
        public IActionResult LoadFormPartial()
        {
            return View("_FormPartial");
        }
    }
}
