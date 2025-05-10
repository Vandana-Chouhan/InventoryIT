using System.Net;
using System.Net.NetworkInformation;
using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryIT.Controllers
{
    public class SupplierMasterController : Controller
    {
        private readonly ISupplierMasterRepository _supplierMasterRepository;
        private readonly IMastBranchRepository _mastBranchRepository;
        private readonly IMastCompRepository _mastCompRepository;
        private readonly IMastStateRepository _mastStateRepository;
        private readonly IMastCityRepository _mastCityRepository;
        public SupplierMasterController(ISupplierMasterRepository supplierMasterRepository,
            IMastBranchRepository mastBranchRepository, IMastCompRepository mastCompRepository,
            IMastStateRepository mastStateRepository, IMastCityRepository mastCityRepository)
        {
            _mastCityRepository = mastCityRepository;
            _mastStateRepository = mastStateRepository;
            _supplierMasterRepository = supplierMasterRepository;
            _mastBranchRepository = mastBranchRepository;
            _mastCompRepository = mastCompRepository;
        }
        public IActionResult supplierMasterDataTable()
        {
            return View("SupplierMasterDataTable");
        }
        public ActionResult GetSupplier()
        {
            var supplier = _supplierMasterRepository.GetAllSupplier();
            var masterData = supplier.Select(c => new
            {
                SuppId = c.SuppId,
                SupplierName = c.SupplierName,
                MobileNo = c.MobileNo,
                CityId = c.CityId,
                Address = c.Address
            }).ToList();
            return Json(new { data = masterData });
        }
        public ActionResult AddSupplierMaster()
        {
            var state = _mastStateRepository.GetAllState();
            ViewBag.MastStates = state.Select(c => new SelectListItem
            {
                Value = c.StateId.ToString(),
                Text = c.StateName
            });
            var city = _mastCityRepository.GetAllMastCity();
            ViewBag.MastCities = city.Select(c => new SelectListItem
            {
                Value = c.CityId.ToString(),
                Text = c.CityName
            });
            return View();
        }
        [HttpPost]
        public ActionResult AddSupplierMaster(SupplierMaster supplierMaster)
        {
            // Retrieve session values as strings
            string? compName = HttpContext.Session.GetString("CompanyName");
            string? branchName = HttpContext.Session.GetString("BranchName");

            int? userId = HttpContext.Session.GetInt32("UserId");

            // Check if session data exists, otherwise redirect to error page
            if (string.IsNullOrEmpty(compName) || string.IsNullOrEmpty(branchName) || userId == null)
            {
                return RedirectToAction("ErrorPage");
            }
            // Example: Fetch corresponding IDs based on names from database or repository
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            if (company != null && branch != null)
            {
                supplierMaster.CompId = company.CompId;
                supplierMaster.BranchId = branch.BranchId;
                supplierMaster.CreatedBy = userId.Value;
            }
            // Create a new supplier master object using the data
            var supplier = new SupplierMaster
            {
                SupplierName = supplierMaster.SupplierName,
                Address = supplierMaster.Address,
                CityId = supplierMaster.CityId,
                ContactPerson = supplierMaster.ContactPerson,
                SupplierGstCertificate = supplierMaster.SupplierGstCertificate,
                PinNo = supplierMaster.PinNo,
                GstNo = supplierMaster.GstNo,
                StateId = supplierMaster.StateId,
                MobileNo = supplierMaster.MobileNo,
                PhoneNo = supplierMaster.PhoneNo,
                EmailId = supplierMaster.EmailId,
                Website = supplierMaster.Website,
                CompId = supplierMaster.CompId,
                BranchId = supplierMaster.BranchId,
                CreatedBy = supplierMaster.CreatedBy
            };
            // Save the supplier master details to the database
            _supplierMasterRepository.AddSupplierMaster(supplier);

            TempData["SuccessMessage"] = "Supplier details saved successfully.";
            // Redirect to another page or show a success message
            return RedirectToAction("AddSupplierMaster", "SupplierMaster");
        }
    }
}