using Azure.Identity;
using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class MasterSetupController : Controller
    {
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly IUserMasterRepository _userMasterRepository;
        public MasterSetupController(IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository, IFinancialYearRepository financialYearRepository, IUserMasterRepository userMasterRepository)
        {
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
            _financialYearRepository = financialYearRepository;
            _userMasterRepository = userMasterRepository;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserMaster userMaster)
        {
            var userDetails = _userMasterRepository.GetAllUserMaster();
            var user = userDetails.FirstOrDefault(x => x.UserName == userMaster.UserName && x.Password == userMaster.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("Session", "MasterSetup");
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View("Login");
        }
        public ActionResult Inventory()
        {
            return View();
        }
        public ActionResult SupplierMaster()
        {
            return View();
        }
        public ActionResult ReturnToView()
        {
            return View();
        }
        public ActionResult Session(int? selectedCompId = null)
        {
            // Get all companies
            var companies = _mastCompRepository.GetAllMastcomp();
            ViewBag.MastComps = companies.Select(c => new SelectListItem
            {
                Value = c.CompId.ToString(),
                Text = c.CompanyName
            });
            // If no company is selected, just render the page with all branches
            if (!selectedCompId.HasValue)
            {
                // Get all branches
                var allBranches = _mastBranchRepository.GetAllMastBranch();
                ViewBag.MastBranches = allBranches.Select(b => new SelectListItem
                {
                    Value = b.BranchId.ToString(),
                    Text = b.BranchName
                });
                // Get all financial years
                var years = _financialYearRepository.GetAllFinancialYear();
                ViewBag.FinancialYears = years.Select(y => new SelectListItem
                {
                    Value = y.FinanYearId.ToString(),
                    Text = y.FinancialYearName
                });
                return View();
            }
            // If a company is selected, filter branches by the company
            var filteredBranches = _mastBranchRepository
                .GetAllMastBranch()
                .Where(b => b.CompId == selectedCompId.Value).ToList();

            var filteredYears = _financialYearRepository.GetAllFinancialYear()
                .Where(y => y.CompId == selectedCompId.Value).ToList();

            // Check if it's an AJAX request by inspecting the "X-Requested-With" header
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var branchList = filteredBranches.Select(b => new
                {
                    Value = b.BranchId.ToString(),
                    Text = b.BranchName
                }).ToList();
                var yearlist = filteredYears.Select(y => new
                {
                    Value = y.FinanYearId.ToString(),
                    Text = y.FinancialYearName
                }).ToList();
                return Json(new { filteredBranches = branchList, filteredYears = yearlist });
            }
            // If not an AJAX request, return the page with all branches
            ViewBag.MastBranches = filteredBranches.Select(b => new SelectListItem
            {
                Value = b.BranchId.ToString(),
                Text = b.BranchName
            });

            ViewBag.FinancialYears = filteredYears.Select(y => new SelectListItem
            {
                Value = y.FinanYearId.ToString(),
                Text = y.FinancialYearName
            });
            return View();
        }
        [HttpPost]
        public ActionResult Session(int CompId, int BranchId, int FinanYearId)
        {
            // Retrieve selected values from the repositories
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompId == CompId);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchId == BranchId);
            var financialYear = _financialYearRepository.GetAllFinancialYear().FirstOrDefault(fy => fy.FinanYearId == FinanYearId);
            // Store selected values in session
            if (company != null)
                HttpContext.Session.SetString("CompanyName", company.CompanyName);
            if (branch != null)
                HttpContext.Session.SetString("BranchName", branch.BranchName);
            if (financialYear != null)
                HttpContext.Session.SetString("FinancialYear", financialYear.FinancialYearName);
            return RedirectToAction("Inventory");
        }
    }
}
