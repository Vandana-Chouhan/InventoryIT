using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class UserMasterController : Controller
    {
        private readonly IUserMasterRepository _userMasterRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        public UserMasterController(IUserMasterRepository userMasterRepository,
         IMastCompRepository mastCompRepository, IMastBranchRepository mastBranchRepository)
        {
            _userMasterRepository = userMasterRepository;
            _mastCompRepository = mastCompRepository;
            _mastBranchRepository = mastBranchRepository;
        }
        public ActionResult AddUserMaster()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserMaster(UserMaster userMaster)
        {
            // Retrieve session values as strings
            string? compName = HttpContext.Session.GetString("CompanyName");
            string? branchName = HttpContext.Session.GetString("BranchName");
            // Check if session data exists, otherwise redirect to error page
            if (string.IsNullOrEmpty(compName) || string.IsNullOrEmpty(branchName))
            {
                return RedirectToAction("ErrorPage");
            }
            // Example: Fetch corresponding IDs based on names from database or repository
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            if (company != null && branch != null)
            {
                userMaster.CompId = company.CompId;
                userMaster.BranchId = branch.BranchId;
            }
            var usermaster = new UserMaster
            {
                UserName = userMaster.UserName,
                Password = userMaster.Password,
                PersonName = userMaster.PersonName,
                UserType = userMaster.UserType,
                //CompanyEmployeeCode= userMaster.CompanyEmployeeCode,
                CompId = userMaster.CompId,
                BranchId = userMaster.BranchId,
            };
            _userMasterRepository.AddUserMaster(usermaster);
            return RedirectToAction("AddUserMaster", "UserMaster");
        }
        public ActionResult ShowUserMasterDataTab()
        {
            return View("UserMasterDataTab");
        }
        public ActionResult GetUser()
        {
            var userdetail = _userMasterRepository.GetAllUserMaster().Select(c => new
            {
                userId = c.UserId,
                userName = c.UserName,
                userType = c.UserType
            }).ToList();
            return Json(new { data = userdetail });
        }
    }
}