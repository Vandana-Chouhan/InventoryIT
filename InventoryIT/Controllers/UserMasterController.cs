using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class UserMasterController : Controller
    {
        private readonly IUserMasterRepository _userMasterRepository;
        public UserMasterController(IUserMasterRepository userMasterRepository)
        {
            _userMasterRepository = userMasterRepository;
        }
        public ActionResult AddUserMaster()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserMaster(UserMaster userMaster)
        {
            _userMasterRepository.AddUserMaster(userMaster);
            return RedirectToAction("AddUserMaster", "UserMaster");
        }
    }
}
