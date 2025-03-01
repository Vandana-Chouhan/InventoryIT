using System.Collections.Generic;
using InventoryIT.Controllers;
using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class MastBranchRepository : IMastBranchRepository
    {
        private readonly InventoryContext _inventoryContext;
        public MastBranchRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        // Fetch all MastBranch records
        public IEnumerable<MastBranch> GetAllMastBranch()
        {
            return _inventoryContext.MastBranches.ToList();
        }
        // Fetch a MastBranch by its ID
        public MastBranch GetbyId(int branchid)
        {
            return _inventoryContext.MastBranches.Find(branchid);
        }
        // Add a new mastbranch to the database
        public int AddFBranchMaster(MastBranch mastBranch)
        {
            int result = 0;
            if (mastBranch != null)
            {
                try
                {
                    _inventoryContext.MastBranches.Add(mastBranch);
                    _inventoryContext.SaveChanges();
                    result = mastBranch.BranchId;  // Assuming CompId is the primary key
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error adding MastBranch", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastBranch), "The mastBranch cannot be null");
            }
            return result;
        }
        // Update an existing MastComp in the database
        public int Update(MastBranch mastBranch)
        {
            int result = -1;
            if (mastBranch != null)
            {
                try
                {
                    _inventoryContext.Entry(mastBranch).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = mastBranch.CompId;
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error updating MastBranch", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastBranch), "The mastBranch cannot be null");
            }
            return result;
        }
        // Delete a MastComp by its ID
        public void Delete(int branchid)
        {
            var mastBranch = _inventoryContext.MastComps.Find(branchid);
            if (mastBranch != null)
            {
                try
                {
                    _inventoryContext.MastComps.Remove(mastBranch);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log exception (depending on your logging mechanism)
                    throw new Exception("Error deleting MastBranch", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"MastBranch with ID {branchid} not found.");
            }
        }
    }
}
