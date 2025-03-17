using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IWarehouseRackRepository
    {
        public IEnumerable<WarehouseRackMaster> GetAllWarehouseRack();
        public IEnumerable<WarehouseRackMaster> GetFilteredWarehouseRack(int companyId, int branchId, int financialYearId);
        public WarehouseRackMaster? GetbyId(int warehouseRackId);
        public int AddWarehouseRack(WarehouseRackMaster warehouseRackMaster);
        public int Update(WarehouseRackMaster warehouseRackMaster);
        public void Delete(int warehouseRackId);
    }
}
