using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Request;
using SmartInventoryBE.ProjectAggregate.Response;

namespace SmartInventoryBE.Mappers;

public static class CategoryMappers
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.Id,
            CategoryName = category.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }

    public static Category ToCategoryFromCreateDTO(this CreateCategoryRequestDto categoryDto)
    {
        return new Category
        {
            Name = categoryDto.CategoryName
        };
    }

    public static Category ToCategoryFromUpdateDTO(this UpdateCategoryRequestDto categoryDto, Category currCategory)
    {
        currCategory.Name = categoryDto.CategoryName;
        currCategory.UpdatedAt = DateTime.Now;
        return currCategory;
    }


}
