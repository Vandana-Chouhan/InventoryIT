using Microsoft.AspNetCore.Mvc;
using InventoryIT.Repository;
using InventoryIT.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class MastBranchController : Controller
    {
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        public MastBranchController(IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository)
        {
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
        }
        public IActionResult AddFBranchMaster()
        {
            var companies = _mastCompRepository.GetAllMastcomp();
            ViewBag.MastComps = companies.Select(c => new SelectListItem
            {
                Value = c.CompId.ToString(),
                Text = c.CompanyName
            });
            return View();
        }
        [HttpPost]
        public IActionResult AddFBranchMaster(MastBranch mastBranch)
        {
            int result = _mastBranchRepository.AddFBranchMaster(mastBranch);
            if (result > 0)
            {
                return RedirectToAction("Inventory", "MasterSetup");
            }
            else
            {
                TempData["Failed"] = "Failed to add the Branch master.";
                return RedirectToAction("AddFBranchMaster");
            }
        }
    }
}
