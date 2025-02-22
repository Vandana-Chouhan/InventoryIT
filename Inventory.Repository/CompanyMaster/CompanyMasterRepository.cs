using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryIT.Models;

namespace Inventory.Repository.CompanyMaster
{
    public class CompanyMasterRepository : BaseRepository,ICompanyMasterRepository
    {
        private readonly ICompanyMasterRepository _companyMasterRepository;
       private CompanyMasterRepository(ICompanyMasterRepository companyMasterRepository)
        {
            _companyMasterRepository = companyMasterRepository;
        }
        public async Task<List<MastComp>> Get()
        {
            var a = await _companyMasterRepository.Get().ConfigureAwait(false);
            return a;
        }
    }
}
