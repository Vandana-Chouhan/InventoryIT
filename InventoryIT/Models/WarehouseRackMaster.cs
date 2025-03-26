using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryIT.Models;

public partial class WarehouseRackMaster
{
    public int WarehouseRackId { get; set; }

    public int WarehouseAreaId { get; set; }

    public int WarehouseLocId { get; set; }

    public string WarehouseRackName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
	[ForeignKey("WarehouseAreaId")]
	public WarehouseAreaMaster WarehouseAreaMaster { get; set; }
	[ForeignKey("WarehouseLocId")]
	public WarehouseLocationMaster WarehouseLocationMaster { get; set; }
}
