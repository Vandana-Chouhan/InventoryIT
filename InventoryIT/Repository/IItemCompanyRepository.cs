using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemCompanytRepository
    {
        public IEnumerable<ItemCompany> GetAllItemCompany();
        public IEnumerable<ItemCompany> GetFilteredItemCompany(int companyId, int branchId, int financialYearId);
        public ItemCompany? GetbyId(int itemcompid);
        public int AddItemCompany(ItemCompany itemCompany);
        public int Update(ItemCompany itemCompany);
        public void Delete(int itemcompid);
    }
}
