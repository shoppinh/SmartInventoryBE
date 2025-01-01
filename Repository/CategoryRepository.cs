using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SmartInventoryContext context) : base(context)
        {
        }

        public IQueryable<Category> Categories => _context.Set<Category>();
    }
}
