using System;
using System.Collections.Generic;

namespace InventoryIT.Models;

public partial class UserMaster
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PersonName { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public int CompId { get; set; }

    public int BranchId { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public DateTime UpdationDateTime { get; set; } = DateTime.Now;
}
