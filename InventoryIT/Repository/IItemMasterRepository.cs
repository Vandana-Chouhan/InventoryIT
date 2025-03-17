using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemMasterRepository
    {
        public IEnumerable<ItemMaster> GetAllItemMaster();
        public IEnumerable<ItemMaster> GetFilteredItemMaster(int companyId, int branchId, int financialYearId);
        public ItemMaster? GetbyId(int imasterid);
        void Save();
        public int AddItemMaster(ItemMaster itemMaster);
        public int Update(ItemMaster itemMaster);
        public void Delete(int itemId);
    }
}
