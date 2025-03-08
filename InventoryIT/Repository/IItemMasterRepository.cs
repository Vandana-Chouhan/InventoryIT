using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemMasterRepository
    {
        public IEnumerable<ItemMaster> GetAllItemMaster();
        public ItemMaster? GetbyId(int imasterid);
        public int AddItemMaster(ItemMaster itemMaster);
        public int Update(ItemMaster itemMaster);
        public void Delete(int itemId);
    }
}
