using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IMastItemStkRepository
    {
        public IEnumerable<MastItemStk> GetAllItemStk();
        public MastItemStk? GetbyId(int stockId);
        void Save();
        public int AddItemStk(MastItemStk mastItemStk);
        public int Update(MastItemStk mastItemStk);
        public void Delete(int stockId);
    }
}
