using System.Collections.Generic;
using InventoryIT.Controllers;
using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class MastCompRepository : IMastCompRepository
    {
        private readonly InventoryContext _inventoryContext;
        public MastCompRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        // Fetch all MastComp records
        public IEnumerable<MastComp> GetAllMastcomp()
        {
            return _inventoryContext.MastComps.ToList();
        }
        // Fetch a MastComp by its ID
        public MastComp? GetbyId(int compid)
        {
            return _inventoryContext.MastComps.Find(compid);
        }
        // Add a new MastComp to the database
        public int AddFCompanyMaster(MastComp mastComp)
        {
            int result = 0;
            if (mastComp != null)
            {
                try
                {
                    _inventoryContext.MastComps.Add(mastComp);
                    _inventoryContext.SaveChanges();
                    result = mastComp.CompId;  // Assuming CompId is the primary key
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error adding MastComp", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastComp), "The mastComp cannot be null");
            }
            return result;
        }
        // Update an existing MastComp in the database
        public int Update(MastComp mastComp)
        {
            int result = -1;
            if (mastComp != null)
            {
                try
                {
                    _inventoryContext.Entry(mastComp).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = mastComp.CompId;
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error updating MastComp", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastComp), "The mastComp cannot be null");
            }
            return result;
        }
        // Delete a MastComp by its ID
        public void Delete(int compid)
        {
            var mastComp = _inventoryContext.MastComps.Find(compid);
            if (mastComp != null)
            {
                try
                {
                    _inventoryContext.MastComps.Remove(mastComp);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error deleting MastComp", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"MastComp with ID {compid} not found.");
            }
        }
    }
}
