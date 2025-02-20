
using InventoryIT.Models;

namespace Inventory.Repository
{
    public abstract class BaseRepository
    {
        protected readonly InventoryContext db = new InventoryContext();
    }
}
