using SmartInventoryBE.Models;

namespace SmartInventoryBE.ProjectAggregate.ViewModel
{
    public class OrderModel : BaseModel
    {
        public required User User { get; set; }
        public DateTime OrderDate { get; set; }
        public required Shipping Shipping { get; set; }
        public required PaymentTransaction PaymentTransaction { get; set; }
        public List<OrderProductsModel> OrderDetails { get; set; } = [];
    }
}
