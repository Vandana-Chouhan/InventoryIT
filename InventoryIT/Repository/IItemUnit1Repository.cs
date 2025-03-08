using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemUnit1Repository
    {
        public IEnumerable<ItemUnit1> GetAllItemUnit1();
        public ItemUnit1? GetbyId(int itemUnitId);
        public int AddItemUnit1(ItemUnit1 itemUnit);
        public int Update(ItemUnit1 itemUnit);
        public void Delete(int itemUnitId);
    }
}
