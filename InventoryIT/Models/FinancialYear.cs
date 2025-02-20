using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class FinancialYear
{
    public int FinanYearId { get; set; }

    public DateOnly FinancialYearName { get; set; }

    public DateOnly FinancialYearFrom { get; set; }

    public DateOnly FinancialYearTo { get; set; }

    public int CompId { get; set; }
}
