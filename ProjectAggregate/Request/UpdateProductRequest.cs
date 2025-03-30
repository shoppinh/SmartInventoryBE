using System;

namespace SmartInventoryBE.ProjectAggregate.Request;

public class UpdateProductRequest
{
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal? ProductPrice { get; set; }
    public int? ProductStock { get; set; }
    public string? ProductImage { get; set; }
    public int? CategoryId { get; set; }
} 