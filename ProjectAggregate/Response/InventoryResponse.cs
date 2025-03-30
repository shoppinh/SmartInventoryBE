namespace SmartInventoryBE.ProjectAggregate.Response
{
    public class InventoryResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductResponse Product { get; set; }
        public int Quantity { get; set; }
        public DateTime RestockDate { get; set; }
    }
} 