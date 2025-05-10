using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IWarehouseAreaRepository
    {
        public IEnumerable<WarehouseAreaMaster> GetAllWarehouseArea();
        public WarehouseAreaMaster? GetbyId(int warehouseAreaId);
        public int AddWarehouseArea(WarehouseAreaMaster warehouseAreaMaster);
        public int Update(WarehouseAreaMaster warehouseAreaMaster);
        public void Delete(int warehouseAreaId);
    }
}
