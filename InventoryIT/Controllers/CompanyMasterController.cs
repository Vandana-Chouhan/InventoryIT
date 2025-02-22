using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class CompanyMasterController : Controller
    {
        //private readonly ICompanyMasterService companyMasterService;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Get()
        {
            return View();
        }
    }
}
