using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace InventoryIT.Models;

public partial class ItemCatagory
{
    public int ItemCatId { get; set; } 

    [Required(ErrorMessage = "Item Category Name is required")]
    [StringLength(10, ErrorMessage = "Item Category Name cannot exceed 10 characters")]
    public string ItemCatagoryName { get; set; } = null!;

    [Required(ErrorMessage = "Company ID is required")]
    public int CompId { get; set; }

    [Required(ErrorMessage = "Branch ID is required")]
    public int BranchId { get; set; }

    [Required(ErrorMessage = "Financial Year ID is required")]
    public int FinanYearId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "CreatedBy is required")]
    public int CreatedBy { get; set; }
}
