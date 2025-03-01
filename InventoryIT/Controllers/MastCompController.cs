using Microsoft.AspNetCore.Mvc;
using InventoryIT.Repository;
using InventoryIT.Models;

namespace InventoryIT.Controllers
{
    public class MastCompController : Controller
    {
        private readonly IMastCompRepository _mastCompRepository;

        public MastCompController(IMastCompRepository mastCompRepository)
        {
            _mastCompRepository = mastCompRepository;
        }
        public IActionResult AddFCompanyMaster()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFCompanyMaster(MastComp mastComp)
        {
            int result = _mastCompRepository.AddFCompanyMaster(mastComp);
            if (result > 0)
            {
                return RedirectToAction("Inventory", "MasterSetup");
            }
            else
            {
                TempData["Failed"] = "Failed to add the company master.";
                return RedirectToAction("AddFCompanyMaster");
            }
        }
    }
}
