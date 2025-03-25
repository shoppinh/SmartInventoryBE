using SmartInventoryBE.Models;

namespace SmartInventoryBE.ProjectAggregate.ViewModel
{
    public class ProductModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ProductStock { get; set; }
        public string Image { get; set; } = string.Empty;
        public required Category Category { get; set; }
        public Inventory? Inventory { get; set; }
        public List<OrderProductsModel> OrderDetails { get; set; } = [];
        public List<Review> Reviews { get; set; } = [];
    }
}
