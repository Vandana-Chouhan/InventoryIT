using System.Collections.Generic;
using InventoryIT.Controllers;
using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly InventoryContext _inventoryContext;
        public ItemTypeRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<ItemType> GetAllItemType()
        {
            return _inventoryContext.ItemTypes.ToList();
        }
        public ItemType? GetbyId(int itemid)
        {
            return _inventoryContext.ItemTypes.Find(itemid);
        }
        public int AddItemType(ItemType itemType)
        {
            int result = 0;
            if (itemType != null)
            {
                try
                {
                    _inventoryContext.ItemTypes.Add(itemType);
                    _inventoryContext.SaveChanges();
                    result = itemType.ItemId;  // Assuming itemId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ItemType Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemType), "The itemtype cannot be null");
            }
            return result;
        }
        public int Update(ItemType itemType)
        {
            int result = -1;
            if (itemType != null)
            {
                try
                {
                    _inventoryContext.Entry(itemType).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = itemType.ItemId;
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error updating itemtype", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemType), "The itemtype cannot be null");
            }
            return result;
        }
        // Delete a ItemType by its ID
        public void Delete(int itemid)
        {
            var itemtype = _inventoryContext.ItemTypes.Find(itemid);
            if (itemtype != null)
            {
                try
                {
                    _inventoryContext.ItemTypes.Remove(itemtype);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting itemtype", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemType with ID {itemid} not found.");
            }
        }
    }
}
