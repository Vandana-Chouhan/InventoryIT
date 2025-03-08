using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class ItemMasterRepository : IItemMasterRepository
    {
        private readonly InventoryContext _inventoryContext;
        public ItemMasterRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<ItemMaster> GetAllItemMaster()
        {
            return _inventoryContext.ItemMasters.ToList();
        }
        public ItemMaster? GetbyId(int imasterid)
        {
            return _inventoryContext.ItemMasters.Find(imasterid);
        }
        public int AddItemMaster(ItemMaster itemMaster)
        {
            int result = 0;
            if (itemMaster != null)
            {
                try
                {
                    _inventoryContext.ItemMasters.Add(itemMaster);
                    _inventoryContext.SaveChanges();
                    result = itemMaster.ItemId;  // Assuming itemmasterid is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ItemMaster Details", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemMaster), "ItemMasterDetails cannot be null");
            }
            return result;
        }
        public int Update(ItemMaster itemMaster)
        {
            int result = -1;
            if (itemMaster != null)
            {
                try
                {
                    _inventoryContext.Entry(itemMaster).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = itemMaster.ItemId;
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error updating itemmasterdetails", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemMaster), "The itemmaster cannot be null");
            }
            return result;
        }
        // Delete a ItemMaster by its ID
        public void Delete(int imasterid)
        {
            var itemMaster = _inventoryContext.ItemMasters.Find(imasterid);
            if (itemMaster != null)
            {
                try
                {
                    _inventoryContext.ItemMasters.Remove(itemMaster);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting itemMaster", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemMaster with ID {imasterid} not found.");
            }
        }
    }
}


