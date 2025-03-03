using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class MastCityRepository : IMastCityRepository
    {
        private readonly InventoryContext _inventoryContext;
        public MastCityRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<MastCity> GetAllMastCity()
        {
            return _inventoryContext.MastCities.ToList();
        }
        public MastCity? GetbyId(int cityId)
        {
            return _inventoryContext.MastCities.Find(cityId);
        }
        public int AddMastCity(MastCity mastCity)
        {
            int result = 0;
            if (mastCity != null)
            {
                try
                {
                    _inventoryContext.MastCities.Add(mastCity);
                    _inventoryContext.SaveChanges();
                    result = mastCity.CityId;  // Assuming cityId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding city Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastCity), "city name cannot be null");
            }
            return result;
        }
        public int Update(MastCity mastCity)
        {
            int result = -1;
            if (mastCity != null)
            {
                try
                {
                    _inventoryContext.Entry(mastCity).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = mastCity.CityId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating cityname", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastCity), " city cannot be null");
            }
            return result;
        }
        public void Delete(int cityId)
        {
            var city = _inventoryContext.MastCities.Find(cityId);
            if (city != null)
            {
                try
                {
                    _inventoryContext.MastCities.Remove(city);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting city", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Cityname with ID {cityId} not found.");
            }
        }
    }
}

  
