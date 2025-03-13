using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemUnitRepository
    {
        public IEnumerable<ItemUnit> GetAllItemUnit();
        public ItemUnit? GetbyId(int itemUnitId);
        public int AddItemUnit(ItemUnit itemUnit);
        public int Update(ItemUnit itemUnit);
        public void Delete(int itemUnitId);
    }
}
