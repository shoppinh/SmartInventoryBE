namespace SmartInventoryBE.Models;

public class Inventory : BaseEntity
{
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; }
    public DateTime RestockDate { get; set; }
}
