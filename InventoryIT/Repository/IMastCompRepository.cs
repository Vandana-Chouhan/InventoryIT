using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IMastCompRepository
    {
        public IEnumerable<MastComp> GetAllMastcomp();
        public MastComp GetbyId(int compid);
        public int AddFCompanyMaster(MastComp mastComp);
        public int Update(MastComp mastComp);
        public void Delete(int compid);
    }
}