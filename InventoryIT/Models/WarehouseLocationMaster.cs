using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryIT.Models;

public partial class WarehouseLocationMaster
{
    public int WarehouseLocId { get; set; }

    [Required(ErrorMessage = "Please enter location name")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Location name must be between 3 and 50 characters.")]
    public string WarehouseName { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public int FinanYearId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
	public ICollection<WarehouseAreaMaster> WarehouseAreaMaster { get; set; }
}
