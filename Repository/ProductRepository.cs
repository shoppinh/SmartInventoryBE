using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Models;

namespace SmartInventoryBE.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(SmartInventoryContext context) : base(context)
        {
        }
        public IQueryable<Product> Products => _context.Set<Product>();
    }
}
