using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class ItemSubCatagoryRepository : IItemSubCatagoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        public ItemSubCatagoryRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<ItemSubCatagory> GetAllItemSubCat()
        {
            return _inventoryContext.ItemSubCatagories.ToList();
        }
        public ItemSubCatagory? GetbyId(int itemSubCatId)
        {
            return _inventoryContext.ItemSubCatagories.Find(itemSubCatId);
        }
        public int AddItemSubCat(ItemSubCatagory itemSubCatagory)
        {
            int result = 0;
            if (itemSubCatagory != null)
            {
                try
                {
                    _inventoryContext.ItemSubCatagories.Add(itemSubCatagory);
                    _inventoryContext.SaveChanges();
                    result = itemSubCatagory.ItemSubCatId;  // Assuming ItemSubCatId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ItemSubCatagory Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemSubCatagory), "The itemsubcatagory cannot be null");
            }
            return result;
        }
        public int Update(ItemSubCatagory itemSubCatagory)
        {
            int result = -1;
            if (itemSubCatagory != null)
            {
                try
                {
                    _inventoryContext.Entry(itemSubCatagory).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = itemSubCatagory.ItemSubCatId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating itemsubcatagory", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemSubCatagory), "The itemsubcatagory cannot be null");
            }
            return result;
        }
        public void Delete(int itemSubCatId)
        {
            var itemtsubcat = _inventoryContext.ItemSubCatagories.Find(itemSubCatId);
            if (itemtsubcat != null)
            {
                try
                {
                    _inventoryContext.ItemSubCatagories.Remove(itemtsubcat);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting itemsubcatagory.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemSubCatagory with ID {itemSubCatId} not found.");
            }
        }
    }
}

