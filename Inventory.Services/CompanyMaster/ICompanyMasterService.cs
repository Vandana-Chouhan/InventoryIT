using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryIT.Models;

namespace Inventory.Services.CompanyMaster
{
    public interface ICompanyMasterService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<MastComp>> Get();
    }
}
