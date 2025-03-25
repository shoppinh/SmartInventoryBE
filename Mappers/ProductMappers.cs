using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;

namespace SmartInventoryBE.Mappers;

public static class ProductMappers
{
    public static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductDescription = product.Description,
            ProductPrice = product.Price,
            ProductStock = product.ProductStock,
            ProductImage = product.Image,
            Category = product.Category.ToCategoryDto(),
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public static Product ToProductFromCreateDTO(this CreateProductRequestDto productDto, Category category)
    {
        return new Product
        {
            Name = productDto.ProductName,
            Description = productDto.ProductDescription,
            Price = productDto.ProductPrice,
            ProductStock = productDto.ProductStock,
            Image = productDto.ProductImage,
            CategoryId = productDto.CategoryId,
            Category = category
        };
    }

    public static Product ToProductFromUpdateDTO(this UpdateProductRequestDto productDto, Product currProduct, Category? category)
    {

        currProduct.Name = productDto.ProductName ?? currProduct.Name;
        currProduct.Description = productDto.ProductDescription ?? currProduct.Description;
        currProduct.Price = productDto.ProductPrice ?? currProduct.Price;
        currProduct.ProductStock = productDto.ProductStock ?? currProduct.ProductStock;
        currProduct.Image = productDto.ProductImage ?? currProduct.Image;
        currProduct.Category = category ?? currProduct.Category;
        currProduct.UpdatedAt = DateTime.Now;
        return currProduct;

    }

}
