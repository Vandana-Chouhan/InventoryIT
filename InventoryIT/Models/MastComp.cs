using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class MastComp
{
    public int CompId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string OwnerName { get; set; } = null!;

    public int City { get; set; }

    public string CompShortName { get; set; } = null!;

    public int? State { get; set; }

    public string? Email { get; set; }

    public decimal? MobileNo { get; set; }

    public decimal? PhoneNo { get; set; }

    public decimal? PinNo { get; set; }

    public string? GstNo { get; set; }

    public string? PanNo { get; set; }

    public string? ContactPerson { get; set; }

    public string? Website { get; set; }

    public int CreatedBy { get; set; }

    public DateTime DateTime { get; set; } = DateTime.Now;
}
