using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class MastCountryRepository : IMastCountryRepository
    {
        private readonly InventoryContext _inventoryContext;
        public MastCountryRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<MastCountry> GetAllCountry()
        {
            return _inventoryContext.MastCountries.ToList();
        }
        public MastCountry? GetbyId(int countryId)
        {
            return _inventoryContext.MastCountries.Find(countryId);
        }
        public int AddCountry(MastCountry mastCountry)
        {
            int result = 0;
            if (mastCountry != null)
            {
                try
                {
                    _inventoryContext.MastCountries.Add(mastCountry);
                    _inventoryContext.SaveChanges();
                    result = mastCountry.CountryId;  // Assuming CountryId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Country Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastCountry), "The country name cannot be null");
            }
            return result;
        }
        public int Update(MastCountry mastCountry)
        {
            int result = -1;
            if (mastCountry != null)
            {
                try
                {
                    _inventoryContext.Entry(mastCountry).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = mastCountry.CountryId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating countryname", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastCountry), "The country name cannot be null");
            }
            return result;
        }
        public void Delete(int countryId)
        {
            var country = _inventoryContext.MastCountries.Find(countryId);
            if (country != null)
            {
                try
                {
                    _inventoryContext.MastCountries.Remove(country);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting countryname", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Countryname  with ID {countryId} not found.");
            }
        }
    }
}

