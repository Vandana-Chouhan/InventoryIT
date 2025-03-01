using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemTypeRepository
    {

        public IEnumerable<ItemType> GetAllItemType();
        public ItemType GetbyId(int itemId);
        public int AddItemType(ItemType itemType);

        public int Update(ItemType itemType);

        public void Delete(int itemId);
    }
}
