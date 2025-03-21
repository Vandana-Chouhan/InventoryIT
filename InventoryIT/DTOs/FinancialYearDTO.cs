using System;
using System.Collections.Generic;

namespace InventoryIT.DTOs;

public  class FinancialYearDTO
{
    public int FinanYearId { get; set; }

    public string FinancialYearName { get; set; } = null!;
    
    public string FinancialYearFrom { get; set; } = null!;

    public string FinancialYearTo { get; set; } = null!;

    public int CompId { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }
}
