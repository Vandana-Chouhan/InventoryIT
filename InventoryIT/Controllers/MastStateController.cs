using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class MastStateController : Controller
    {
        public IActionResult AddState()
        {
            return View();
        }
    }
}
