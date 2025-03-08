using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class MastItemSupplierRate
{
    public int EntryId { get; set; }

    public int SuppId { get; set; }

    public double SupplierRate { get; set; }

    public int ItemId { get; set; }

    public string SupplierCompanyName { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public virtual ItemMaster Item { get; set; } = null!;
}
