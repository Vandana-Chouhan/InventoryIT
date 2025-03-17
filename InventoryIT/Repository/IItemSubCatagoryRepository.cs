using InventoryIT.Models;

namespace InventoryIT.Repository
{
    public interface IItemSubCatagoryRepository
    {
        public IEnumerable<ItemSubCatagory> GetAllItemSubCat();
        public IEnumerable<ItemSubCatagory> GetFilteredItemSubCat(int companyId, int branchId, int financialYearId);
        public ItemSubCatagory? GetbyId(int itemSubCatId);
        public int AddItemSubCat(ItemSubCatagory itemSubCatagory);
        public int Update(ItemSubCatagory itemSubCatagory);
        public void Delete(int itemSubCatId);
    }
}
