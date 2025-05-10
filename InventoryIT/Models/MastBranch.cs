using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class MastBranch
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string BranchShortName { get; set; } = null!;

    public string OwnerName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int City { get; set; }

    public int? State { get; set; }

    public decimal? MobileNo { get; set; }

    public decimal? PhoneNo { get; set; }

    public string? PanNo { get; set; }

    public string? GstNo { get; set; }

    public decimal? PinNo { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? ContactPerson { get; set; }

    public int CompId { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }

    public virtual MastComp Comp { get; set; } = null!;
}
