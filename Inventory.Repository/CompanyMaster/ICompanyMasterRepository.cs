using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryIT.Models;

namespace Inventory.Repository.CompanyMaster
{
    interface ICompanyMasterRepository
    {
        Task<List<MastComp>> Get();
    }
}
