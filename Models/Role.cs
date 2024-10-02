using System;

namespace SmartInventoryBE.Models;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public List<User> Users { get; set; } = new List<User>();
    public List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
