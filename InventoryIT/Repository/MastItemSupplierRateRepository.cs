using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class MastItemSupplierRateRepository : IMastItemSupplierRateRepository
    {
        private readonly InventoryContext _inventoryContext;
        public MastItemSupplierRateRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<MastItemSupplierRate> GetAllItemSupplierRate()
        {
            return _inventoryContext.MastItemSupplierRates.ToList();
        }
        public MastItemSupplierRate? GetbyId(int suppId)
        {
            return _inventoryContext.MastItemSupplierRates.Find(suppId);
        }
        public int AddItemSupplierRate(MastItemSupplierRate mastItemSupplierRate)
        {
            int result = 0;
            if (mastItemSupplierRate != null)
            {
                try
                {
                    _inventoryContext.MastItemSupplierRates.Add(mastItemSupplierRate);
                    _inventoryContext.SaveChanges();
                    result = mastItemSupplierRate.SuppId; // Assuming ItemMastSuppID is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ItemSupplierRates Details", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastItemSupplierRate), "ItemSupplierRates cannot be null");
            }
            return result;
        }
        public int Update(MastItemSupplierRate mastItemSupplierRate)
        {
            int result = -1;
            if (mastItemSupplierRate != null)
            {
                try
                {
                    _inventoryContext.Entry(mastItemSupplierRate).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = mastItemSupplierRate.SuppId;
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error updating ItemSupplierRates", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastItemSupplierRate), "The ItemSupplierRates cannot be null");
            }
            return result;
        }
        // Delete ItemSupplierRates by its ID
        public void Delete(int suppId)
        {
            var suppRate = _inventoryContext.MastItemSupplierRates.Find(suppId);
            if (suppRate != null)
            {
                try
                {
                    _inventoryContext.MastItemSupplierRates.Remove(suppRate);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting ItemSupplierRates Details.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemSupplierRates with ID {suppId} not found.");
            }
        }
    }
}

