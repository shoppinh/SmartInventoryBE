using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartInventoryBE.Extensions;
using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Models;
using SmartInventoryBE.ProjectAggregate.Search;
using SmartInventoryBE.Settings;

namespace SmartInventoryBE.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly GeneralSettings _generalSettings;
        protected readonly SmartInventoryContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(IOptions<GeneralSettings> generalSettings, SmartInventoryContext dbContent)
        {
            _generalSettings = generalSettings.Value;
            _context = dbContent;
            _dbSet = _context.Set<TEntity>();
        }

        protected IQueryable<TEntity> InitializeQueryAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }
        protected virtual IQueryable<TEntity> Includes()
        {
            return InitializeQueryAsNoTracking();
        }
        protected IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = InitializeQueryAsNoTracking();

            if (includes == null)
            {
                return query;
            }

            query = includes.Aggregate(query, (current, include) => current.Include(include.AsPath()));

            return query;
        }

        Task IGenericRepository<TEntity>.AddAsync(IList<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.AddOrUpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.AddOrUpdateAsync(IList<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<TEntity>.CountAsync(SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<TEntity>.CountAsync(Expression<Func<TEntity, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.DeleteAsync(Expression<Func<TEntity, bool>> criteria, bool isSoftDeleted)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.DeleteAsync(int id, bool isSoftDeleted)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.DeleteAsync(IList<int> ids, bool isSoftDeleted)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.DeleteAsync(IList<TEntity> entities, bool isSoftDeleted)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.DeleteAsync(TEntity entity, bool isSoftDeleted)
        {
            throw new NotImplementedException();
        }

        Task<TEntity?> IGenericRepository<TEntity>.GetAsync(Expression<Func<TEntity, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        Task<TEntity?> IGenericRepository<TEntity>.GetAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[]? includes)
        {
            throw new NotImplementedException();
        }

        Task<TEntity?> IGenericRepository<TEntity>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<TEntity?> IGenericRepository<TEntity>.GetByIdAsync(int id, params Expression<Func<TEntity, object>>[]? includes)
        {
            throw new NotImplementedException();
        }

        Task<IList<TEntity>> IGenericRepository<TEntity>.GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        Task<IList<TEntity>> IGenericRepository<TEntity>.GetByIdsAsync(IList<int> ids, params Expression<Func<TEntity, object>>[]? includes)
        {
            throw new NotImplementedException();
        }

        Task<IList<TDestination>> IGenericRepository<TEntity>.GetByIdsProjectionAsync<TDestination>(IList<int> ids, Expression<Func<TEntity, TDestination>> projection)
        {
            throw new NotImplementedException();
        }

        Task<TDestination?> IGenericRepository<TEntity>.GetWithProjectionAsync<TDestination>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TDestination>> projection) where TDestination : default
        {
            throw new NotImplementedException();
        }

        Task<IList<TEntity>> IGenericRepository<TEntity>.ListAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[]? includes)
        {
            throw new NotImplementedException();
        }

        Task<IList<TEntity>> IGenericRepository<TEntity>.ListAsync(Expression<Func<TEntity, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        Task<IList<TDestination>> IGenericRepository<TEntity>.ListWithProjectionAsync<TDestination>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TDestination>> projection)
        {
            throw new NotImplementedException();
        }

        Task<IList<TEntity>> IGenericRepository<TEntity>.SearchAllAsync(SearchCriteria criteria, params Expression<Func<TEntity, object>>[]? includes)
        {
            throw new NotImplementedException();
        }

        Task<IList<TEntity>> IGenericRepository<TEntity>.SearchAllAsync(SearchCriteria? criteria)
        {
            throw new NotImplementedException();
        }

        Task<IList<TDestination>> IGenericRepository<TEntity>.SearchAllWithProjectionASync<TDestination>(Expression<Func<TEntity, TDestination>> projection, SearchCriteria? criteria)
        {
            throw new NotImplementedException();
        }

        Task<GenericSearchResult<TEntity>> IGenericRepository<TEntity>.SearchAsync(SearchCriteria? criteria)
        {
            throw new NotImplementedException();
        }

        Task<GenericSearchResult<TEntity>> IGenericRepository<TEntity>.SearchAsync(SearchCriteria criteria, params Expression<Func<TEntity, object>>[]? includes)
        {
            throw new NotImplementedException();
        }

        Task<GenericSearchResult<TDestination>> IGenericRepository<TEntity>.SearchWithProjectionAsync<TDestination>(Expression<Func<TEntity, TDestination>> projection, SearchCriteria? criteria)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.UpdateAsync(IList<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<TEntity>.UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
