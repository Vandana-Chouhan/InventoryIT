using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class MastStateRepository : IMastStateRepository
    {
        private readonly InventoryContext _inventoryContext;

        public MastStateRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }
        public IEnumerable<MastState> GetAllState()
        {
            return _inventoryContext.MastStates.ToList();
        }
        public MastState? GetbyId(int stateId)
        {
            return _inventoryContext.MastStates.Find(stateId);
        }
        public int AddState(MastState mastState)
        {
            int result = 0;
            if (mastState!= null)
            {
                try
                {
                    _inventoryContext.MastStates.Add(mastState);
                    _inventoryContext.SaveChanges();
                    result = mastState.StateId;  // Assuming StateId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding State Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastState), "The state name cannot be null");
            }
            return result;
        }
        public int Update(MastState mastState)
        {
            int result = -1;
            if (mastState != null)
            {
                try
                {
                    _inventoryContext.Entry(mastState).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = mastState.StateId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Statename", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(mastState), "The State name cannot be null");
            }
            return result;
        }
        public void Delete(int stateId)
        {
            var state = _inventoryContext.MastStates.Find(stateId);
            if (state != null)
            {
                try
                {
                    _inventoryContext.MastStates.Remove(state);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting statename", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Statename  with ID {stateId} not found.");
            }
        }
    }
}


