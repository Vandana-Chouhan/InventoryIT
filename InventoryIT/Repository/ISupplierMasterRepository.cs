using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface ISupplierMasterRepository
    {
        public IEnumerable<SupplierMaster> GetAllSupplier();
        public IEnumerable<SupplierMaster> GetFilteredSupplier(int companyId, int branchId);
        public SupplierMaster? GetbyId(int suppId);
        public int AddSupplierMaster(SupplierMaster supplierMaster);
        public int Update(SupplierMaster supplierMaster);
        public void Delete(int suppId);
    }
}
