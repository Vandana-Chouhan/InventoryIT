using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IMastCityRepository
    {
        public IEnumerable<MastCity> GetAllMastCity();
        public MastCity? GetbyId(int cityId);
        public int AddMastCity(MastCity mastCity);
        public int Update(MastCity mastCity);
        public void Delete(int cityId);
    }
}
