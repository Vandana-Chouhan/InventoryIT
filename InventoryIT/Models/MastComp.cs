using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number. It must contain exactly 10 digits.")]
    [Required(ErrorMessage = "Mobile Number is required.")]

    public string? MobileNo { get; set; }
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number. It must contain exactly 10 digits.")]
    [Required(ErrorMessage = "Phone Number is required.")]

    public string? PhoneNo { get; set; }

    public decimal? PinNo { get; set; }

    public string? GstNo { get; set; }

    public string? PanNo { get; set; }

    public string? ContactPerson { get; set; }

    public string? Website { get; set; }

    public int CreatedBy { get; set; }

    public DateTime DateTime { get; set; } = DateTime.Now;

    public virtual ICollection<FinancialYear> FinancialYears { get; set; } = new List<FinancialYear>();

    public virtual ICollection<MastBranch> MastBranches { get; set; } = new List<MastBranch>();
}
