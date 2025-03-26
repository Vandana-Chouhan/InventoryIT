using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IMastCountryRepository
    {
        public IEnumerable<MastCountry> GetAllCountry();
        public MastCountry? GetbyId(int countryId);
        public int AddCountry( MastCountry mastCountry);
        public int Update(MastCountry mastCountry);
        public void Delete(int countryId);
    }
}
