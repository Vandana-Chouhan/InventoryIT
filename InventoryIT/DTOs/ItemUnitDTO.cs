using System;
using System.Collections.Generic;

namespace InventoryIT.DTOs;

public class ItemUnitDTO
{
    public int ItemUnitId { get; set; }

    public string ItemUnitName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
}
