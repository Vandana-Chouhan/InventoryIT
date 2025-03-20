using System;
using System.Collections.Generic;

namespace InventoryIT.DTOs;

public class ItemSubCatagoryDTO
{
    public int ItemSubCatId { get; set; }

    public string SubCatagoryName { get; set; } = null!;

    public int ItemMainCatId { get; set; }

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
}
