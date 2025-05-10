using System.Collections.Generic;
using InventoryIT.Controllers;
using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class FinancialYearRepository : IFinancialYearRepository
    {
        private readonly InventoryContext _inventoryContext;
        public FinancialYearRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<FinancialYear> GetAllFinancialYear()
        {
            return _inventoryContext.FinancialYears.ToList();
        }
        public FinancialYear? GetbyId(int finanid)
        {
            return _inventoryContext.FinancialYears.Find(finanid);
        }
        public int AddFinancialYear(FinancialYear financialYear)
        {
            int result = 0;
            if (financialYear != null)
            {
                try
                {
                    _inventoryContext.FinancialYears.Add(financialYear);
                    _inventoryContext.SaveChanges();
                    result = financialYear.FinanYearId;  // Assuming CompId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Financial Year", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(financialYear), "The Financial Year cannot be null");
            }
            return result;
        }
        public int Update(FinancialYear financialYear)
        {
            int result = -1;
            if (financialYear != null)
            {
                try
                {
                    _inventoryContext.Entry(financialYear).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = financialYear.FinanYearId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Financial Year", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(financialYear), "The Financial  Year cannot be null");
            }
            return result;
        }
        public void Delete(int finanid)
        {
            var year = _inventoryContext.FinancialYears.Find(finanid);
            if (year != null)
            {
                try
                {
                    _inventoryContext.FinancialYears.Remove(year);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Financial year details", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Financial Year  with ID {finanid} not found.");
            }
        }
    }
}