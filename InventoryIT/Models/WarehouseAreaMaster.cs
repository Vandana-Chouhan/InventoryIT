using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class WarehouseAreaMaster
{
    public int WarehouseLocId { get; set; }

    public int WarehouseAreaId { get; set; }

    public string WarehouseAreaName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
}
