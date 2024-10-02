using System;

namespace SmartInventoryBE.Models;

public class RolePermission
{
    public int RolePermissionId { get; set; }
    public int RoleId { get; set; }
    public required Role Role { get; set; }
    public int PermissionId { get; set; }
    public required Permission Permission { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
