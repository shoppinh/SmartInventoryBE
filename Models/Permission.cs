using System;

namespace SmartInventoryBE.Models;

public class Permission
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
