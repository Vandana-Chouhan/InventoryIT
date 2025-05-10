using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class MasterDataController : Controller
    {
        public IActionResult MasterDataCard()
        {
            return View("MasterDataCardsPartial");
        }
        public IActionResult LoadFormPartial()
        {
            return View("_FormPartial");
        }
    }
}
