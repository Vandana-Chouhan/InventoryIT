using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class ItemType
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinancialYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
}
