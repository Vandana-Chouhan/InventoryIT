using InventoryIT.Models;
namespace InventoryIT.ViewModels
{
    public class ItemMasterViewModel
    {
        public ItemMaster itemMaster { get; set; } = null!;
        public List<MastItemSupplierRate> supplierRate { get; set; } = null!;// Change this from a single supplierRate to a list
        public MastItemStk mastItemStk { get; set; } = null!;
    }
}