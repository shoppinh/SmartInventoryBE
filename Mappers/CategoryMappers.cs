using System;
using SmartInventoryBE.Dtos.Category;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Mappers;

public static class CategoryMappers
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }

    public static Category ToCategoryFromCreateDTO(this CreateCategoryRequestDto categoryDto)
    {
        return new Category
        {
            CategoryName = categoryDto.CategoryName
        };
    }

    public static Category ToCategoryFromUpdateDTO(this UpdateCategoryRequestDto categoryDto, Category currCategory)
    {
        currCategory.CategoryName = categoryDto.CategoryName;
        currCategory.UpdatedAt = DateTime.Now;
        return currCategory;
    }


}
