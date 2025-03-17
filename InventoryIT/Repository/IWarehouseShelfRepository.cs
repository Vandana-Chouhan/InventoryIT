using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IWarehouseShelfRepository
    {
        public IEnumerable<WarehouseShelfMaster> GetAllWarehouseShelf();
        public IEnumerable<WarehouseShelfMaster> GetFilteredWarehouseShelf(int companyId, int branchId, int financialYearId);
        public WarehouseShelfMaster? GetbyId(int warehouseShelfId);
        public int AddWarehouseShelf(WarehouseShelfMaster warehouseShelfMaster);
        public int Update(WarehouseShelfMaster warehouseShelfMaster);
        public void Delete(int warehouseShelfId);
    }
}
