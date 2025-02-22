using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repository.CompanyMaster;
using InventoryIT.Models;

namespace Inventory.Services.CompanyMaster
{
    public class CompanyMasterService : ICompanyMasterService
    {
        private readonly CompanyMasterRepository _companyMasterRepository;
        private CompanyMasterService(CompanyMasterRepository companyMasterRepository)
        {
            _companyMasterRepository = companyMasterRepository;
        }
        public async Task<List<MastComp>> Get()
        {
            var s = await _companyMasterRepository.Get().ConfigureAwait(false);
            return s;
        }
    }
}
