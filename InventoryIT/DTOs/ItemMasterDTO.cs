using System;
using System.Collections.Generic;

namespace InventoryIT.DTOs;

public class ItemMasterDTO
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public int ItemType { get; set; }

    public int ItemCompany { get; set; }

    public int ItemCatagory { get; set; }

    public int ItemSubCatagory { get; set; }

    public int ItemUnit1 { get; set; }

    public int ItemUnit2 { get; set; }

    public int WarehouseLocation { get; set; }

    public int WarehouseArea { get; set; }

    public int WarehouseRack { get; set; }

    public int WarehouseShelf { get; set; }

    public int? CompId { get; set; }

    public int? BranchId { get; set; }

    public int? FinanYearId { get; set; }

    public string? PartNo { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreationDateTime { get; set; } = DateTime.Now;

    public virtual ICollection<MastItemStkDTO> MastItemStks { get; set; } = new List<MastItemStkDTO>();

    public virtual ICollection<MastItemSupplierRateDTO> MastItemSupplierRates { get; set; } = new List<MastItemSupplierRateDTO>();
}
