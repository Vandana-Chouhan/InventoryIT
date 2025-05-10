using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class MastItemStk
{
    public int EntryId { get; set; }

    public int StockId { get; set; }

    public int ItemId { get; set; }

    public double? OpeningQuantity { get; set; }

    public double? CurrentQuantity { get; set; }

    public double? ClosingQuantity { get; set; }

    public double? OpeningValue { get; set; }

    public double? Gst { get; set; }

    public double? PurchaseRate { get; set; }

    public double? SalesRate { get; set; }

    public double? BufferStock { get; set; }

    public int? CompId { get; set; }

    public int? BranchId { get; set; }

    public int? FinanYearId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreationDateTime { get; set; } = DateTime.Now;

    public virtual ItemMaster Item { get; set; } = null!;
}
