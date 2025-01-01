using SmartInventoryBE.ProjectAggregate.ViewModel;

namespace SmartInventoryBE.Models;

public class Category : GenericBaseEntity<CategoryModel, Category>
{
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new List<Product>();
}
