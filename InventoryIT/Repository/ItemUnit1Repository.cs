using System.Collections.Generic;
using InventoryIT.Controllers;
using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class ItemUnit1Repository : IItemUnit1Repository
    {
        private readonly InventoryContext _inventoryContext;
        public ItemUnit1Repository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<ItemUnit1> GetAllItemUnit1()
        {
            return _inventoryContext.ItemUnits.ToList();
        }
        public ItemUnit1? GetbyId(int itemUnitId)
        {
            return _inventoryContext.ItemUnits.Find(itemUnitId);
        }
        public int AddItemUnit1(ItemUnit1 itemUnit)
        {
            int result = 0;
            if (itemUnit != null)
            {
                try
                {
                    _inventoryContext.ItemUnits.Add(itemUnit);
                    _inventoryContext.SaveChanges();
                    result = itemUnit.ItemUnitId1;  // Assuming ItemUnitId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding ItemUnit Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemUnit), "The itemtunit cannot be null");
            }
            return result;
        }
        public int Update(ItemUnit1 itemUnit)
        {
            int result = -1;
            if (itemUnit != null)
            {
                try
                {
                    _inventoryContext.Entry(itemUnit).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = itemUnit.ItemUnitId1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating itemunit", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemUnit), "The itemtunit cannot be null");
            }
            return result;
        }
        public void Delete(int itemUnitId)
        {
            var itemtunit = _inventoryContext.ItemTypes.Find(itemUnitId);
            if (itemtunit != null)
            {
                try
                {
                    _inventoryContext.ItemTypes.Remove(itemtunit);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting itemunit", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemUnit with ID {itemUnitId} not found.");
            }
        }
    }
}