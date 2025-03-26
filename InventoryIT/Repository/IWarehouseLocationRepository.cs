using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IWarehouseLocationRepository
    {
        public string GetWarehouseLocationName(int locationId);

        public IEnumerable<WarehouseLocationMaster> GetAllWarehouseLocation();
        public WarehouseLocationMaster? GetbyId(int warehouseLocId);
        public int AddWarehouselocation(WarehouseLocationMaster warehouseLocationMaster);
        public int Update(WarehouseLocationMaster warehouseLocationMaster);
        public void Delete(int warehouseLocId);
        public IEnumerable<WarehouseLocationMaster> GetFilteredWarehouseLocations(int companyId, int branchId, int financialYearId);
    }
}
