using SmartInventoryBE.Models;

namespace SmartInventoryBE.Interfaces.RepositoryInterfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<Category> Categories { get; }
    }
}
