using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class MastItemStkRepository : IMastItemStkRepository
    {
        private readonly InventoryContext _inventoryContext;
        public MastItemStkRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<MastItemStk> GetAllItemStk()
        {
            return _inventoryContext.MastItemStks.ToList();
        }
        public MastItemStk? GetbyId(int stockId)
        {
            return _inventoryContext.MastItemStks.Find(stockId);
        }
        public int AddItemStk(MastItemStk mastItemStk)
        {
            int result = 0;
            if (mastItemStk != null)
            {
                try
                {
                    _inventoryContext.MastItemStks.Add(mastItemStk);
                    _inventoryContext.SaveChanges();
                    result = mastItemStk.StockId; // Assuming ItemMastStkId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ItemMasterStk Details", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastItemStk), "ItemMasterStkDetails cannot be null");
            }
            return result;
        }
        public int Update(MastItemStk mastItemStk)
        {
            int result = -1;
            if (mastItemStk != null)
            {
                try
                {
                    _inventoryContext.Entry(mastItemStk).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = mastItemStk.StockId;
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error updating itemStkdetails", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastItemStk), "The itemMasterStk cannot be null");
            }
            return result;
        }
        // Delete a ItemMastStk by its ID
        public void Delete(int stockId)
        {
            var masterStk = _inventoryContext.MastItemStks.Find(stockId);
            if (masterStk != null)
            {
                try
                {
                    _inventoryContext.MastItemStks.Remove(masterStk);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting itemMasterStk Details.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemMasterStk with ID {stockId} not found.");
            }
        }
    }
}

