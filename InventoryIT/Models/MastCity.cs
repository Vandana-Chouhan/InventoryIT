using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class MastCity
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public string? CityShortName { get; set; }

    public int StateId { get; set; }

    public string? PinCode { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreationDatetime { get; set; } = DateTime.Now;

    public virtual MastState State { get; set; } = null!;
}
