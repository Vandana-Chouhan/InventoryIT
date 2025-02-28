using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IMastBranchRepository
    {
        public IEnumerable<MastBranch> GetAllMastBranch();
        public MastBranch GetbyId(int branchid);
        public int AddFBranchMaster(MastBranch mastBranch);

        public int Update(MastBranch mastBranch);

        public void Delete(int branchid);
    }
}