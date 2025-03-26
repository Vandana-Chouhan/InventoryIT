using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IWarehouseAreaRepository
    {
        public string GetWarehouseAreaName(int areaId);

        public IEnumerable<WarehouseAreaMaster> GetAllWarehouseArea();
        public IEnumerable<WarehouseAreaMaster> GetFilteredWarehouseArea(int companyId, int branchId, int financialYearId);
        public WarehouseAreaMaster? GetbyId(int warehouseAreaId);
        public int AddWarehouseArea(WarehouseAreaMaster warehouseAreaMaster);
        public int Update(WarehouseAreaMaster warehouseAreaMaster);
        public void Delete(int warehouseAreaId);
    }
}
