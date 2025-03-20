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
        private readonly IMastStateRepository _mastStateRepository;
        private readonly IMastCityRepository _mastCityRepository;
        public MastBranchController(IMastBranchRepository mastBranchRepository,
            IMastCompRepository mastCompRepository, IMastStateRepository mastStateRepository,
            IMastCityRepository mastCityRepository)
        {
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _mastStateRepository = mastStateRepository;
            _mastCityRepository = mastCityRepository;
        }
        public ActionResult AddFBranchMaster()
        {
            var companies = _mastCompRepository.GetAllMastcomp();
            ViewBag.MastComps = companies.Select(c => new SelectListItem
            {
                Value = c.CompId.ToString(),
                Text = c.CompanyName
            });
            ViewBag.MastStates = _mastStateRepository.GetAllState().Select(c => new SelectListItem
            {
                Value = c.StateId.ToString(),
                Text = c.StateName
            });
            return View();
        }
        // Action to fetch city by selected state
        [HttpGet]
        public JsonResult GetCityByState(int stateid)
        {
            var city = _mastCityRepository.GetAllMastCity()
                                            .Where(a => a.StateId == stateid)
                                            .Select(c => new SelectListItem
                                            {
                                                Value = c.CityId.ToString(),
                                                Text = c.CityName
                                            }).ToList();
            return Json(city); // Return city as JSON
        }
        [HttpPost]
        public ActionResult AddFBranchMaster(MastBranch mastBranch)
        {
            int result = _mastBranchRepository.AddFBranchMaster(mastBranch);
            if (result > 0)
            {
                return RedirectToAction("AddFBranchMaster", "MastBranch");
            }
            else
            {
                TempData["Failed"] = "Failed to add the Branch master.";
                return RedirectToAction("AddFBranchMaster", "MastBranch");
            }
        }
        public IActionResult ShowMastBranchDataTable()
        {
            return View("mastBranchDataTab");
        }
        [HttpGet]
        public IActionResult GetAllBranch()
        {
            try
            {
                var branch = _mastBranchRepository.GetAllMastBranch();
                var cities = _mastCityRepository.GetAllMastCity().ToList();
                var company = _mastCompRepository.GetAllMastcomp().ToList();
                var branchData = branch.Select(c => new
                {
                    BranchId = c.BranchId,
                    BranchName = c.BranchName,
                    CompanyName = company.FirstOrDefault(comp => comp.CompId == c.CompId)?.CompanyName,
                    Address = c.Address,
                    City = cities.FirstOrDefault(city => city.CityId == c.City)?.CityName,
                    MobileNo = c.MobileNo
                }).ToList();
                return Json(new { data = branchData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Server Error", message = ex.Message });
            }
        }
    }
}
