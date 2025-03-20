using System;
using System.Collections.Generic;

namespace InventoryIT.DTOs;

public class SupplierMasterDTO
{
    public int SuppId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int CityId { get; set; }

    public string? ContactPerson { get; set; }

    public byte[]? SupplierGstCertificate { get; set; }

    public string? PinNo { get; set; }

    public string? GstNo { get; set; }

    public int? StateId { get; set; }

    public decimal? MobileNo { get; set; }

    public decimal? PhoneNo { get; set; }

    public string? EmailId { get; set; }

    public string? Website { get; set; }

    public int? CompId { get; set; }

    public int? BranchId { get; set; }

    public DateTime? CreationDateTime { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }
}
