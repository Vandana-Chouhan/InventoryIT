using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryIT.Models;

public partial class WarehouseShelfMaster
{
    public int WarehouseShelfId { get; set; }

    public int WarehouseRackId { get; set; }

    public int WarehouseAreaId { get; set; }

    public int WarehouseLocId { get; set; }

    public string WarehouseShelfName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
	[ForeignKey("WarehouseLocId")]
	public WarehouseLocationMaster WarehouseLocationMaster { get; set; }
	[ForeignKey("WarehouseAreaId")]
	public WarehouseAreaMaster WarehouseAreaMaster { get; set; }
	[ForeignKey("WarehouseRackId")]
	public WarehouseRackMaster WarehouseRackMaster { get; set; }
}
