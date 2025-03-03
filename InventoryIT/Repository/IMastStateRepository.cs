using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IMastStateRepository
    {
        public IEnumerable<MastState> GetAllState();
        public MastState? GetbyId(int stateId);
        public int AddState(MastState mastState);
        public int Update(MastState mastState);
        public void Delete(int stateId);
    }
}
