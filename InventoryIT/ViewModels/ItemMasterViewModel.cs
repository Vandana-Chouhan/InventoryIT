using InventoryIT.Models;
namespace InventoryIT.ViewModels
{
    public class ItemMasterViewModel
    {
        public ItemMaster itemMaster { get; set; } = null!;
        public MastItemSupplierRate supplierRate { get; set; } = null!;
        public MastItemStk mastItemStk { get; set; } = null!;
    }
}