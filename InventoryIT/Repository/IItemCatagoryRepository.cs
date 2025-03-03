using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemCatagoryRepository
    {
        public IEnumerable<ItemCatagory> GetAllItemCatagory();
        public ItemCatagory? GetbyId(int itemCatId);
        public int AddItemCatagory(ItemCatagory itemCatagory);
        public int Update(ItemCatagory itemCatagory);
        public void Delete(int itemCatId);
    }
}
