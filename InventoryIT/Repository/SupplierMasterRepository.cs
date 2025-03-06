using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class SupplierMasterRepository : ISupplierMasterRepository
    {
        private readonly InventoryContext _inventoryContext;
        public SupplierMasterRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<SupplierMaster> GetAllSupplier()
        {
            return _inventoryContext.SupplierMasters.ToList();
        }
        public SupplierMaster? GetbyId(int SuppId)
        {
            return _inventoryContext.SupplierMasters.Find(SuppId);
        }
        public int AddSupplierMaster(SupplierMaster supplierMaster)
        {
            int result = 0;
            if (supplierMaster != null)
            {
                try
                {
                    _inventoryContext.SupplierMasters.Add(supplierMaster);
                    _inventoryContext.SaveChanges();
                    result = supplierMaster.SuppId;  // Assuming SuppId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding supplier details.", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(supplierMaster), "supplier details cannot be null");
            }
            return result;
        }
        public int Update(SupplierMaster supplierMaster)
        {
            int result = -1;
            if (supplierMaster != null)
            {
                try
                {
                    _inventoryContext.Entry(supplierMaster).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = supplierMaster.SuppId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating supplier details.", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(supplierMaster), "supplier details cannot be null");
            }
            return result;
        }
        public void Delete(int SuppId)
        {
            var supplier = _inventoryContext.SupplierMasters.Find(SuppId);
            if (supplier != null)
            {
                try
                {
                    _inventoryContext.SupplierMasters.Remove(supplier);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting supplier master details", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"supplier details with ID {SuppId} not found.");
            }
        }
    }
}

