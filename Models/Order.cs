using System;
using System.ComponentModel.DataAnnotations;
namespace SmartInventoryBE.Models;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int ShippingId { get; set; }
    public required Shipping Shipping { get; set; } 
    public int PaymentTransactionId { get; set; }
    public required PaymentTransaction PaymentTransaction { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();


}
