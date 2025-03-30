namespace SmartInventoryBE.ProjectAggregate.Request
{
    public class UpdateInventoryRequest
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? RestockDate { get; set; }
    }
} 