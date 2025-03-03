using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class MastState
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public string? StateShortName { get; set; }

    public int CountryId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreationDatetime { get; set; } = DateTime.Now;

    public virtual MastCountry Country { get; set; } = null!;

    public virtual ICollection<MastCity> MastCities { get; set; } = new List<MastCity>();
}
