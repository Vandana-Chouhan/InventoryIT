using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class SupplierMasterController : Controller
    {
        public IActionResult AddSupplierMaster()
        {
            return View();
        }
    }
}
