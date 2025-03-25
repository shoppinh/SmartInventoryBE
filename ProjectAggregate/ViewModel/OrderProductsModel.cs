namespace SmartInventoryBE.ProjectAggregate.ViewModel
{
    public class OrderProductsModel : BaseModel
    {
        public required OrderModel Order { get; set; }
        public required ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
