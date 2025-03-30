namespace SmartInventoryBE.ProjectAggregate.Request
{
    public class CreateInventoryRequest
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required string RestockDate { get; set; }
    }
} 