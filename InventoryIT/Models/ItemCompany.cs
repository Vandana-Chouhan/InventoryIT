using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class ItemCompany
{
    public int ItemComId { get; set; }

    public string ItemCompanyName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
}
