using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class WarehouseLocationRepository : IWarehouseLocationRepository
    {
        private readonly InventoryContext _inventoryContext;
        public WarehouseLocationRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<WarehouseLocationMaster> GetAllWarehouseLocation()
        {
            return _inventoryContext.WarehouseLocationMasters.ToList();
        }
        public IEnumerable<WarehouseLocationMaster> GetFilteredWarehouseLocations(int companyId, int branchId, int financialYearId)
        {
            return _inventoryContext.WarehouseLocationMasters
           .Where(loc => loc.CompId == companyId && loc.BranchId == branchId && loc.FinanYearId == financialYearId)
           .ToList();
        }
        public WarehouseLocationMaster? GetbyId(int warehouseLocId)
        {
            return _inventoryContext.WarehouseLocationMasters.Find(warehouseLocId);
        }
        public int AddWarehouselocation(WarehouseLocationMaster warehouseLocationMaster)
        {
            int result = 0;
            if (warehouseLocationMaster != null)
            {
                try
                {
                    _inventoryContext.WarehouseLocationMasters.Add(warehouseLocationMaster);
                    _inventoryContext.SaveChanges();
                    result = warehouseLocationMaster.WarehouseLocId;  // Assuming LocationId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding WarehouseLocation Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseLocationMaster), "Warehouselocation name cannot be null");
            }
            return result;
        }
        public int Update(WarehouseLocationMaster warehouseLocationMaster)
        {
            int result = -1;
            if (warehouseLocationMaster != null)
            {
                try
                {
                    _inventoryContext.Entry(warehouseLocationMaster).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = warehouseLocationMaster.WarehouseLocId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Warehouse location name.", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseLocationMaster), "Warehouse location name cannot be null");
            }
            return result;
        }
        public void Delete(int warehouseLocId)
        {
            var locname = _inventoryContext.WarehouseLocationMasters.Find(warehouseLocId);
            if (locname != null)
            {
                try
                {
                    _inventoryContext.WarehouseLocationMasters.Remove(locname);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Warehouse location details", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Warehouse location with ID {warehouseLocId} not found.");
            }
        }
    }
}