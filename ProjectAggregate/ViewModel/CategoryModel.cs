using SmartInventoryBE.Models;

namespace SmartInventoryBE.ProjectAggregate.ViewModel
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
