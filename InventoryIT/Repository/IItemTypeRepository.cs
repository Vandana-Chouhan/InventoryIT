using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemTypeRepository
    {
        public IEnumerable<ItemType> GetAllItemType();
        public IEnumerable<ItemType> GetFilteredItemType(int companyId, int branchId, int financialYearId);
        public ItemType? GetbyId(int itemId);
        public int AddItemType(ItemType itemType);
        public int Update(ItemType itemType);
        public void Delete(int itemId);
    }
}
