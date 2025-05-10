using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class WarehouseShelfRepository : IWarehouseShelfRepository
    {

        private readonly InventoryContext _inventoryContext;
        public WarehouseShelfRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<WarehouseShelfMaster> GetAllWarehouseShelf()
        {
            return _inventoryContext.WarehouseShelfMasters.ToList();
        }
        public WarehouseShelfMaster? GetbyId(int warehouseShelfId)
        {
            return _inventoryContext.WarehouseShelfMasters.Find(warehouseShelfId);
        }
        public int AddWarehouseShelf(WarehouseShelfMaster warehouseShelfMaster)
        {
            int result = 0;
            if (warehouseShelfMaster != null)
            {
                try
                {
                    _inventoryContext.WarehouseShelfMasters.Add(warehouseShelfMaster);
                    _inventoryContext.SaveChanges();
                    result = warehouseShelfMaster.WarehouseShelfId;  // Assuming warehouse shelf ID is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding warehouse shelf Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseShelfMaster), "Warehouse shelf details cannot be null");
            }
            return result;
        }
        public int Update(WarehouseShelfMaster warehouseShelfMaster)
        {
            int result = -1;
            if (warehouseShelfMaster != null)
            {
                try
                {
                    _inventoryContext.Entry(warehouseShelfMaster).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = warehouseShelfMaster.WarehouseShelfId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating warehouse shelf details.", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(warehouseShelfMaster), "warehouse shelf details cannot be null");
            }
            return result;
        }
        public void Delete(int warehouseShelfId)
        {
            var warehouseshelf = _inventoryContext.WarehouseShelfMasters.Find(warehouseShelfId);
            if (warehouseshelf != null)
            {
                try
                {
                    _inventoryContext.WarehouseShelfMasters.Remove(warehouseshelf);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting warehouse shelf details.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($" warehouse shelf details with ID {warehouseShelfId} not found.");
            }
        }
    }
}