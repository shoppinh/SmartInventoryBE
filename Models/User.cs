using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventoryBE.Models;

public class User : IdentityUser
{
    [Column(TypeName = "varchar(200)")]
    public string FirstName { get; set; } = string.Empty;
    [Column(TypeName = "varchar(200)")]
    public string LastName { get; set; } = string.Empty;
    [Column(TypeName = "varchar(20)")]
    public string Phone { get; set; } = string.Empty;
    [Column(TypeName = "varchar(200)")]
    public string Address { get; set; } = string.Empty;
    [Column(TypeName = "varchar(50)")]
    public string City { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Order> Orders { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
    public List<Log> Logs { get; set; } = [];
}
