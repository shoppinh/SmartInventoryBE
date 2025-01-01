using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Interfaces;

namespace SmartInventoryBE.Repository
{
    public class DbContextUnitOfWork : IUnitOfWork
    {
        public DbContext DbContext { get; private set; }

        public DbContextUnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
