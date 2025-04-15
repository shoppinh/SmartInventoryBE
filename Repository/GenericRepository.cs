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
        protected readonly GeneralSettings GeneralSettings;
        protected readonly SmartInventoryContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected GenericRepository(IOptions<GeneralSettings> generalSettings, SmartInventoryContext dbContent)
        {
            GeneralSettings = generalSettings.Value;
            Context = dbContent;
            DbSet = Context.Set<TEntity>();
        }

        protected IQueryable<TEntity> InitializeQueryAsNoTracking()
        {
            return DbSet.AsNoTracking();
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

        public virtual async Task<IList<TEntity>>  SearchAllAsync(SearchCriteria criteria, params Expression<Func<TEntity, object>>[]? includes)
        {
            return await HandleSearchAllAsync(criteria, Query(includes));
        }

        public virtual async Task<IList<TEntity>> SearchAllAsync(SearchCriteria? criteria = null)
        {
            return await HandleSearchAllAsync(criteria ?? new SearchCriteria(), Includes());
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

        /// <summary>
        /// Builds and modifies the provided query based on the given search criteria. 
        /// This method can be overridden to apply additional query modifications like filtering, sorting, etc.
        /// </summary>
        /// <param name="query">
        /// The initial query to modify.
        /// </param>
        /// <param name="criteria">
        /// The search criteria used to modify the query (e.g., filtering, sorting).
        /// </param>
        /// <returns>
        /// A modified query based on the search criteria.
        /// </returns>
        protected virtual IQueryable<TEntity> BuildQuery(IQueryable<TEntity> query, SearchCriteria criteria)
        {
            return query;
        }

        protected virtual IQueryable<TEntity> BuildSort(IQueryable<TEntity> query, SearchCriteria criteria)
        {
            return query;
        }

        private async Task<GenericSearchResult<TEntity>> HandleSearchAsync(SearchCriteria criteria, IQueryable<TEntity> query)
        {
            query = BuildQuery(query, criteria);
            var result = new GenericSearchResult<TEntity>();
            result.TotalCount = await query.CountAsync();
            if (result.TotalCount == 0)
            {
                return result;
            }

            query = BuildSort(query, criteria);
            result.Results = await query.Skip(criteria.Skip).Take(criteria.Take).ToListAsync();
            return result;
        }

        private async Task<IList<TEntity>> HandleSearchAllAsync(SearchCriteria criteria, IQueryable<TEntity> query)
        {
            var result = new List<TEntity>();
            criteria.Take = GeneralSettings.BatchSize;

            query = BuildQuery(query, criteria);
            var totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                return result;
            }

            query = BuildSort(query, criteria);
            var count = 0;

            do
            {
                criteria.Skip = count;
                var entities = await query.Skip(criteria.Skip).Take(criteria.Take).ToListAsync();
                if (!entities.SafeAny())
                {
                    break;
                }
                count += entities.Count;
                result.AddRange(entities);
            }
            while (count < totalCount);

            return result;
        }
    }
}