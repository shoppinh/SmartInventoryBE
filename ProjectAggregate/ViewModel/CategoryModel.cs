namespace SmartInventoryBE.ProjectAggregate.ViewModel
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public List<ProductModel> Products { get; set; } = [];
    }
}
