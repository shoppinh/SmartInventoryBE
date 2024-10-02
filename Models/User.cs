using System;

namespace SmartInventoryBE.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int RoleId { get; set; }
    public required Role Role { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
    public List<Review> Reviews { get; set; } = new List<Review>();
    public List<Log> Logs { get; set; } = new List<Log>();
}
