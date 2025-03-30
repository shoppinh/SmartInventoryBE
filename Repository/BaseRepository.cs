using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Extensions;
using SmartInventoryBE.Interfaces;
using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Models;
using System.Linq.Expressions;

namespace SmartInventoryBE.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly SmartInventoryContext _context;

        public IUnitOfWork UnitOfWork { get; private set; }

        public BaseRepository(SmartInventoryContext context, IUnitOfWork unitOfWork = null)
        {
            _context = context;
            UnitOfWork = unitOfWork ?? new DbContextUnitOfWork(context);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> conditions, IList<string> navigationPropertyPaths = null)
        {
            return await _context.Set<T>().AsNoTracking().IncludeFilteredNavigation(navigationPropertyPaths).Where(conditions).Where(x => !x.IsDeleted).CountAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> conditions)
        {
            return await _context.Set<T>().Where(x => !x.IsDeleted).Where(conditions).FirstOrDefaultAsync();
        }

        public IQueryable<T> IncludeRelatedData(IQueryable<T> query, List<string> navigationPropertyPaths = null)
        {
            if (navigationPropertyPaths != null)
            {
                foreach (string path in navigationPropertyPaths)
                {
                    query = query.IncludeIgnoreDeleted(path);
                }
            }
            return query;
        }

        public void SetStates(EntityState state, bool removeEntity = false, params T[] entities)
        {
            foreach (T entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;

                if (state == EntityState.Deleted)
                {
                    if (removeEntity)
                        _context.Entry(entity).State = EntityState.Deleted;
                    else
                    {
                        entity.IsDeleted = true;
                        _context.Entry(entity).State = EntityState.Modified;
                    }
                }
                else
                {
                    _context.Entry(entity).State = state;
                }
            }
        }

        void IRepository<T>.AddWithoutCommit<TItem>(TItem item)
        {
            _context.Add(item);
        }

        public async Task<int> DeleteAsync(T entity, bool isRemoved)
        {
            if (isRemoved)
            {
                _context.Entry(entity).State = EntityState.Deleted;
                return await _context.SaveChangesAsync();
            }
            else
            {
                entity.IsDeleted = true;
                return await UpdateAsync(entity);
            }
        }

        public async Task<int> DeleteManyAsync(IEnumerable<T> entities, bool isRemoved)
        {
            if (isRemoved)
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                }
                return await _context.SaveChangesAsync();

            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                }
                return await UpdateManyAsync(entities);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> conditions, int skip, int take, IList<string> navigationPropertyPaths, Dictionary<string, bool> sortValue)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .IncludeFilteredNavigation(navigationPropertyPaths)
                .Where(conditions)
                .Where(x => !x.IsDeleted)
                .ApplySorting(sortValue)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            return model is { IsDeleted: true } ? null : model;
        }

        public async Task<T> GetByIdAsync(int id, List<string> includes)
        {
            return await Query(includes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual IQueryable<T> Query()
        {
            return _context.Set<T>().Where(x => !x.IsDeleted);
        }
        public IQueryable<T> Query(List<string> navigationPropertyPaths)
        {
            return IncludeRelatedData(Query(), navigationPropertyPaths);
        }

        public async Task<T> InsertAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> InsertManyAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return await _context.SaveChangesAsync();
        }

        public void RemoveWithoutCommit<TItem>(TItem item) where TItem : class
        {
            _context.Remove(item);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<int> UpdateManyAsync(IEnumerable<T> entities)
        {
            var utcNow = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.UpdatedAt = utcNow;
                _context.Entry(entity).State = EntityState.Modified;
            }

            return await _context.SaveChangesAsync();
        }

        public void UpdateWithoutCommit<TItem>(TItem item) where TItem : class
        {
            _context.Update(item);
            _context.Entry(item).State = EntityState.Modified;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
                UnitOfWork = null;
            }
        }
    }
}
