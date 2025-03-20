using System;
using System.Collections.Generic;

namespace InventoryIT.DTOs;

public class MastStateDTO
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public string? StateShortName { get; set; }

    public int CountryId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreationDatetime { get; set; } = DateTime.Now;

    public virtual MastCountryDTO Country { get; set; } = null!;

    public virtual ICollection<MastCityDTO> MastCities { get; set; } = new List<MastCityDTO>();
}
