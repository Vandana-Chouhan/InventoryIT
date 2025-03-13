using System.Collections.Generic;
using InventoryIT.Controllers;
using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class ItemUnitRepository : IItemUnitRepository
    {
        private readonly InventoryContext _inventoryContext;
        public ItemUnitRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<ItemUnit> GetAllItemUnit()
        {
            return _inventoryContext.ItemUnits.ToList();
        }
        public ItemUnit? GetbyId(int itemUnitId)
        {
            return _inventoryContext.ItemUnits.Find(itemUnitId);
        }
        public int AddItemUnit(ItemUnit itemUnit)
        {
            int result = 0;
            if (itemUnit != null)
            {
                try
                {
                    _inventoryContext.ItemUnits.Add(itemUnit);
                    _inventoryContext.SaveChanges();
                    result = itemUnit.ItemUnitId;  // Assuming ItemUnitId is the primary key
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
        public int Update(ItemUnit itemUnit)
        {
            int result = -1;
            if (itemUnit != null)
            {
                try
                {
                    _inventoryContext.Entry(itemUnit).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = itemUnit.ItemUnitId;
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