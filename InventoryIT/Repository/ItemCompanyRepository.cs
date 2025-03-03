using InventoryIT.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Repository
{
    public class ItemCompanyRepository : IItemCompanytRepository
    {
        private readonly InventoryContext _inventoryContext;
        public ItemCompanyRepository(InventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }

        public IEnumerable<ItemCompany> GetAllItemCompany()
        {
            return _inventoryContext.ItemCompanies.ToList();
        }

        public ItemCompany? GetbyId(int itemcompid)
        {
            return _inventoryContext.ItemCompanies.Find(itemcompid);
        }

        public int AddItemCompany(ItemCompany itemCompany)
        {
            int result = 0;
            if (itemCompany != null)
            {
                try
                {
                    _inventoryContext.ItemCompanies.Add(itemCompany);
                    _inventoryContext.SaveChanges();
                    result = itemCompany.ItemComId;  // Assuming ItemComId is the primary key
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Itemcompany Name", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemCompany), "The itemtcompany cannot be null");
            }
            return result;
        }
        public int Update(ItemCompany itemCompany)
        {
            int result = -1;
            if (itemCompany != null)
            {
                try
                {
                    _inventoryContext.Entry(itemCompany).State = EntityState.Modified;
                    _inventoryContext.SaveChanges();
                    result = itemCompany.ItemComId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating itemcompany", ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(itemCompany), "The itemcompany cannot be null");
            }
            return result;
        }

        public void Delete(int itemcompid)
        {
            var itemtcomp = _inventoryContext.ItemCompanies.Find(itemcompid);
            if (itemtcomp != null)
            {
                try
                {
                    _inventoryContext.ItemCompanies.Remove(itemtcomp);
                    _inventoryContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting itemcompany", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException($"ItemCompany with ID {itemcompid} not found.");
            }
        }
    }
}

