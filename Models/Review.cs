namespace SmartInventoryBE.Models;

public class Review : BaseEntity
{
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public required User User { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; }

}
