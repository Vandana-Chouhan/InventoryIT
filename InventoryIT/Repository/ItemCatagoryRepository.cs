using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class ItemCatagoryRepository : IItemCatagoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        public ItemCatagoryRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<ItemCatagory> GetAllItemCatagory()
        {
            return _inventoryContext.ItemCatagories.ToList();
        }
        public ItemCatagory? GetbyId(int itemCatId)
        {
            return _inventoryContext.ItemCatagories.Find(itemCatId);
        }
        public int AddItemCatagory(ItemCatagory itemCatagory)
        {
            int result = 0;
            if (itemCatagory != null)
            {
                try
                {
                    _inventoryContext.ItemCatagories.Add(itemCatagory);
                    _inventoryContext.SaveChanges();
                    result = itemCatagory.ItemCatId;  // Assuming ItemcatId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Itemcatagory Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemCatagory), "The  itemcatagory cannot be null");
            }
            return result;
        }
        public int Update(ItemCatagory itemCatagory)
        {
            int result = -1;
            if (itemCatagory != null)
            {
                try
                {
                    _inventoryContext.Entry(itemCatagory).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = itemCatagory.ItemCatId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating itemcatagory", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemCatagory), "The itemcatagory cannot be null");
            }
            return result;
        }
        public void Delete(int itemCatId)
        {
            var itemtcat = _inventoryContext.ItemCatagories.Find(itemCatId);
            if (itemtcat != null)
            {
                try
                {
                    _inventoryContext.ItemCatagories.Remove(itemtcat);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting itemcatagory", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemCatagory with ID {itemCatId} not found.");
            }
        }
    }
}