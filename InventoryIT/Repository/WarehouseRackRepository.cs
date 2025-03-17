using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class WarehouseRackRepository : IWarehouseRackRepository
    {
        private readonly InventoryContext _inventoryContext;
        public WarehouseRackRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<WarehouseRackMaster> GetAllWarehouseRack()
        {
            return _inventoryContext.WarehouseRackMasters.ToList();
        }
        public IEnumerable<WarehouseRackMaster> GetFilteredWarehouseRack(int companyId, int branchId, int financialYearId)
        {
            return _inventoryContext.WarehouseRackMasters
           .Where(loc => loc.CompId == companyId && loc.BranchId == branchId && loc.FinanYearId == financialYearId)
           .ToList();
        }
        public WarehouseRackMaster? GetbyId(int warehouseRackId)
        {
            return _inventoryContext.WarehouseRackMasters.Find(warehouseRackId);
        }
        public int AddWarehouseRack(WarehouseRackMaster warehouseRackMaster)
        {
            int result = 0;
            if (warehouseRackMaster != null)
            {
                try
                {
                    _inventoryContext.WarehouseRackMasters.Add(warehouseRackMaster);
                    _inventoryContext.SaveChanges();
                    result = warehouseRackMaster.WarehouseRackId;  // Assuming warehouse rack ID is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding warehouse rack Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseRackMaster), "Warehouse rack details cannot be null");
            }
            return result;
        }
        public int Update(WarehouseRackMaster warehouseRackMaster)
        {
            int result = -1;
            if (warehouseRackMaster != null)
            {
                try
                {
                    _inventoryContext.Entry(warehouseRackMaster).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = warehouseRackMaster.WarehouseRackId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating warehouse rack details.", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseRackMaster), "warehouse rack details cannot be null");
            }
            return result;
        }
        public void Delete(int warehouseRackId)
        {
            var warehouserack = _inventoryContext.WarehouseRackMasters.Find(warehouseRackId);
            if (warehouserack != null)
            {
                try
                {
                    _inventoryContext.WarehouseRackMasters.Remove(warehouserack);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting warehouse rack details.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($" warehouse rack details with ID {warehouseRackId} not found.");
            }
        }
    }
}