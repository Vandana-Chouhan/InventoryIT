using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class PurchaseMgmtController : Controller
    {
        public IActionResult MaterialRequest()
        {
            return View();
        }
        public IActionResult PurchaseOrder()
        {
            return View();
        }
        public IActionResult GRN()
        {
            return View();
        }
        public IActionResult ItemIssue()
        {
            return View();
        }
        public IActionResult ItemReturn()
        {
            return View();
        }
        public IActionResult ItemStockUpdate()
        {
            return View();
        }
        public IActionResult ShowTranctionCards()
        {
            return View("TransactionCards");
        }
    }
}
