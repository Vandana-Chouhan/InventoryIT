using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class WarehouseAreaRepository : IWarehouseAreaRepository
    {
        private readonly InventoryContext _inventoryContext;
        public WarehouseAreaRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public string GetWarehouseAreaName(int areaId)
        {
            return _inventoryContext.WarehouseAreaMasters
                   .Where(a => a.WarehouseAreaId == areaId)
                   .Select(a => a.WarehouseAreaName)
                   .FirstOrDefault() ?? "Unknown";
        }
        public IEnumerable<WarehouseAreaMaster> GetAllWarehouseArea()
        {
            return _inventoryContext.WarehouseAreaMasters.ToList();
        }
        public IEnumerable<WarehouseAreaMaster> GetFilteredWarehouseArea(int companyId, int branchId, int financialYearId)
        {
            return _inventoryContext.WarehouseAreaMasters
           .Where(loc => loc.CompId == companyId && loc.BranchId == branchId && loc.FinanYearId == financialYearId)
           .ToList();
        }
        public WarehouseAreaMaster? GetbyId(int warehouseAreaId)
        {
            return _inventoryContext.WarehouseAreaMasters.Find(warehouseAreaId);
        }
        public int AddWarehouseArea(WarehouseAreaMaster warehouseAreaMaster)
        {
            int result = 0;
            if (warehouseAreaMaster != null)
            {
                try
                {
                    _inventoryContext.WarehouseAreaMasters.Add(warehouseAreaMaster);
                    _inventoryContext.SaveChanges();
                    result = warehouseAreaMaster.WarehouseAreaId;  // Assuming Warehouse Area id is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Warehouse Area Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseAreaMaster), "Warehouse area master cannot be null");
            }
            return result;
        }
        public int Update(WarehouseAreaMaster warehouseAreaMaster)
        {
            int result = -1;
            if (warehouseAreaMaster != null)
            {
                try
                {
                    _inventoryContext.Entry(warehouseAreaMaster).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = warehouseAreaMaster.WarehouseAreaId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating warehouse area details.", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseAreaMaster), "Warehouse area details cannot be null");
            }
            return result;
        }
        public void Delete(int warehouseAreaId)
        {
            var warehousearea = _inventoryContext.WarehouseAreaMasters.Find(warehouseAreaId);
            if (warehousearea != null)
            {
                try
                {
                    _inventoryContext.WarehouseAreaMasters.Remove(warehousearea);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting warehouse area details.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Warehouse area with ID {warehouseAreaId} not found.");
            }
        }
    }
}