using SmartInventoryBE.Models;

namespace SmartInventoryBE.Interfaces.RepositoryInterfaces
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        IQueryable<Inventory> Inventories { get; }
    }
}
