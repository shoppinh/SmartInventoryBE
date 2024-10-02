using System;

namespace SmartInventoryBE.Dtos.Product;

public class CreateProductRequestDto
{
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public required decimal ProductPrice { get; set; }
    public required int ProductStock { get; set; }
    public required string ProductImage { get; set; }
    public required int CategoryId { get; set; }
}
