using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IMastItemSupplierRateRepository
    {
        public IEnumerable<MastItemSupplierRate> GetAllItemSupplierRate();
        public MastItemSupplierRate? GetbyId(int suppId);
        public int AddItemSupplierRate(MastItemSupplierRate mastItemSupplierRate);
        public int Update(MastItemSupplierRate mastItemSupplierRate);
        public void Delete(int suppId);
        void Save();
    }
}
