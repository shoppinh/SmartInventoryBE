using SmartInventoryBE.ProjectAggregate.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventoryBE.Models;

[Table("OrderProducts")]
public class OrderProduct : GenericBaseEntity<OrderProductsModel, OrderProduct>
{
    public int OrderId { get; set; }
    public required Order Order { get; set; }
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

}
