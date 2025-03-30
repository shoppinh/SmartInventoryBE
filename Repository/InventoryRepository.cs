using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Repository
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(SmartInventoryContext context) : base(context)
        {
        }

        public IQueryable<Inventory> Inventories => _context.Set<Inventory>();

        // public new async Task<IEnumerable<Inventory>> GetAllAsync()
        // {
        //     return await Inventories.Include(x => x.Product).ThenInclude(x => x.Category).Where(x => !x.IsDeleted).ToListAsync();
        // }
    }
}
