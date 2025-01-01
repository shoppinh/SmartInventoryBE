using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Models;
using System.Linq.Expressions;

namespace SmartInventoryBE.Interfaces.RepositoryInterfaces
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<T> Query();
        IQueryable<T> Query(List<string> navigationPropertyPaths);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> conditions,
            int skip,
            int take,
            IList<string> navigationPropertyPaths = null,
            Dictionary<string, bool> sortValue = null);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, List<string> includes);
        Task<T> InsertAsync(T entity);
        Task<int> InsertManyAsync(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateManyAsync(IEnumerable<T> entities);
        Task<int> DeleteAsync(T entity, bool isRemoved = false);
        Task<int> DeleteManyAsync(IEnumerable<T> entities, bool isRemoved = false);
        Task<int> SaveChangesAsync();
        void AddWithoutCommit<TItem>(TItem item) where TItem : class;
        void UpdateWithoutCommit<TItem>(TItem item) where TItem : class;
        void RemoveWithoutCommit<TItem>(TItem item) where TItem : class;
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> conditions);
        Task<int> CountAsync(Expression<Func<T, bool>> conditions, IList<string> navigationPropertyPaths = null);
        void SetStates(EntityState state, bool removeEntity = false, params T[] entities);
        IQueryable<T> IncludeRelatedData(IQueryable<T> query, List<string> navigationPropertyPaths = null);

    }
}
