using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class MasterDataController : Controller
    {
        public IActionResult Index()
        {
            return PartialView("_MasterDataCardsPartial");
        }
        public IActionResult LoadFormPartial()
        {
            return PartialView("_FormPartial");
        }
    }
}
