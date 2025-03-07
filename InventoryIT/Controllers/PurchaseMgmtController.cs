using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class PurchaseMgmtController : Controller
    {
        public IActionResult MaterialRequest()
        {
            return PartialView("MaterialRequest");
        }
        public IActionResult PurchaseOrder()
        {
            return PartialView("PurchaseOrder");
        }
        public IActionResult GRN()
        {
            return PartialView("GRN");
        }
        public IActionResult ItemIssue()
        {
            return PartialView("ItemIssue");
        }
        public IActionResult ItemReturn()
        {
            return PartialView("ItemReturn");
        }
        public IActionResult ItemStockUpdate()
        {
            return PartialView("ItemStockUpdate");
        }
        public IActionResult LoadTransactionCard()
        {
            return PartialView("TransactionCards");
        }
    }
}
