using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventoryBE.Models;

public class PaymentTransaction
{
    public int PaymentTransactionId { get; set; }
    public required Order Order { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
