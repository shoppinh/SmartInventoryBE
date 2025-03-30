using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventoryBE.Models;

[Table("Orders")]
public class Order : BaseEntity
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public DateTime OrderDate { get; set; }
    public int ShippingId { get; set; }
    public required Shipping Shipping { get; set; }
    public int PaymentTransactionId { get; set; }
    public required PaymentTransaction PaymentTransaction { get; set; }
    public List<OrderProduct> OrderDetails { get; set; } = [];


}
