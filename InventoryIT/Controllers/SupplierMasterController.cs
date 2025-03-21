using System.Net;
using System.Net.NetworkInformation;
using InventoryIT.Models;
using InventoryIT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            // Retrieve session values
            string? compName = HttpContext.Session.GetString("CompanyName");
            string? branchName = HttpContext.Session.GetString("BranchName");

            if (string.IsNullOrEmpty(compName) || string.IsNullOrEmpty(branchName))
            {
                return Json(new { data = new List<object>() });
            }
            // Fetch company, branch, and financial year IDs based on session values
            var company = _mastCompRepository.GetAllMastcomp().FirstOrDefault(c => c.CompanyName == compName);
            var branch = _mastBranchRepository.GetAllMastBranch().FirstOrDefault(b => b.BranchName == branchName);
            // If the company, branch, or financial year is not found, return empty data
            if (company == null || branch == null)
            {
                return Json(new { data = new List<object>() });
            }
            // Call the repository method to get filtered locations directly
            var cities = _mastCityRepository.GetAllMastCity().ToList();
            var supplier = _supplierMasterRepository.GetFilteredSupplier(company.CompId, branch.BranchId)
                .Select(c => new
                {
                    suppId = c.SuppId,
                    supplierName = c.SupplierName,
                    mobileNo = c.MobileNo,
                    city = cities.FirstOrDefault(city => city.CityId == c.CityId)?.CityName,
                    address = c.Address
                }).ToList();
            return Json(new { data = supplier });
        }
        public ActionResult AddSupplierMaster()
        {
            var state = _mastStateRepository.GetAllState();
            ViewBag.MastStates = state.Select(c => new SelectListItem
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
        public IActionResult SaveCity(MastCity mastCity)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                mastCity.CreatedBy = userId.Value;
            }
            var city = new MastCity
            {
                CityName = mastCity.CityName,
                StateId = mastCity.StateId,
                CreatedBy = mastCity.CreatedBy
            };
            // Save the city details to the database
            _mastCityRepository.AddMastCity(city);
            // Optionally, you can redirect to a success page or return a response
            return RedirectToAction("AddSupplierMaster", "SupplierMaster");  // Adjust as per your needs
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