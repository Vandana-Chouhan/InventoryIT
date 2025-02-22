using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class ItemSetUpController : Controller
    {
       
        public IActionResult ItemType()
        {
            return View();
        }
        public IActionResult ItemUnit()
        {
            return View();
        }
        public IActionResult ItemCompany()
        {
            return View();
        }
        public IActionResult ItemCatagory()
        {
            return View();
        }
        public IActionResult ItemSubCatagory()
        {
            return View();
        }
    }
}
