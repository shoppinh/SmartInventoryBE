using SmartInventoryBE.Models;

namespace SmartInventoryBE.Interfaces.RepositoryInterfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> Products { get; }
    }
}
