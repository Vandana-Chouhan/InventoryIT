using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class WarehouseController : Controller
    {
        public IActionResult WarehouseLocation()
        {
            return View();
        }
        public IActionResult WarehouseArea()
        {
            return View();
        }
        public IActionResult WarehouseRack()
        {
            return View();
        }
        public IActionResult WarehouseShelf()
        {
            return View();
        }
    }
}
