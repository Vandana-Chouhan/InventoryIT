using System;
using System.Collections.Generic;

namespace InventoryIT.DTOs;

public class MastCountryDTO
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string? CountryShortName { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public virtual ICollection<MastStateDTO> MastStates { get; set; } = new List<MastStateDTO>();
}
