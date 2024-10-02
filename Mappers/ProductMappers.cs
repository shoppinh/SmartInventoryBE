using System;
using SmartInventoryBE.Dtos.Product;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Mappers;

public static class ProductMappers
{
    public static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            ProductPrice = product.ProductPrice,
            ProductStock = product.ProductStock,
            ProductImage = product.ProductImage,
            Category = product.Category.ToCategoryDto(),
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public static Product ToProductFromCreateDTO(this CreateProductRequestDto productDto, Category category)
    {
        return new Product
        {
            ProductName = productDto.ProductName,
            ProductDescription = productDto.ProductDescription,
            ProductPrice = productDto.ProductPrice,
            ProductStock = productDto.ProductStock,
            ProductImage = productDto.ProductImage,
            CategoryId = productDto.CategoryId,
            Category = category
        };
    }

    public static Product ToProductFromUpdateDTO(this UpdateProductRequestDto productDto, Product currProduct, Category? category)
    {

        currProduct.ProductName = productDto.ProductName ?? currProduct.ProductName;
        currProduct.ProductDescription = productDto.ProductDescription ?? currProduct.ProductDescription;
        currProduct.ProductPrice = productDto.ProductPrice ?? currProduct.ProductPrice;
        currProduct.ProductStock = productDto.ProductStock ?? currProduct.ProductStock;
        currProduct.ProductImage = productDto.ProductImage ?? currProduct.ProductImage;
        currProduct.Category = category ?? currProduct.Category;
        currProduct.UpdatedAt = DateTime.Now;
        return currProduct;

    }

}
