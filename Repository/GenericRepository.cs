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
        protected readonly SmartInventoryContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected GenericRepository(IOptions<GeneralSettings> generalSettings, SmartInventoryContext dbContent)
        {
            GeneralSettings = generalSettings.Value;
            DbContext = dbContent;
            DbSet = DbContext.Set<TEntity>();
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

        public virtual async Task AddAsync(IList<TEntity> entities)
        {
            if (!entities.SafeAny())
            {
                return;
            }

            foreach (var batch in entities.Batches(GeneralSettings.BatchSize))
            {
                await AddBatchAsync(batch);
            }
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task AddOrUpdateAsync(TEntity entity)
        {
            await CreateOrUpdateEntryAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task AddOrUpdateAsync(IList<TEntity> entities)
        {
            if (!entities.SafeAny())
            {
                return;
            }
            foreach (var batch in entities.Batches(GeneralSettings.BatchSize))
            {
                await AddOrUpdateBatchAsync(batch);
            }

        }

        public virtual async Task<int> CountAsync(SearchCriteria criteria)
        {
            var query = InitializeQueryAsNoTracking();
            query = BuildQuery(query, criteria);
            return await query.CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await InitializeQueryAsNoTracking().Where(criteria).CountAsync();
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> criteria, bool isSoftDeleted = false)
        {
            if (isSoftDeleted)
            {
                await DbSet.Where(criteria).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
                return;

            }

            await DbSet.Where(criteria).ExecuteDeleteAsync();
        }

        public virtual async Task DeleteAsync(int id, bool isSoftDeleted = false)
        {
            if (isSoftDeleted)
            {
                await DbSet.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
                return;
            }
            await DbSet.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public virtual async Task DeleteAsync(IList<int> ids, bool isSoftDeleted = false)
        {
            if (isSoftDeleted)
            {
                await DbSet.Where(x => ids.Contains(x.Id)).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
                return;
            }

            await DbSet.Where(x => ids.Contains(x.Id)).ExecuteDeleteAsync();
        }

        public virtual async Task DeleteAsync(IList<TEntity> entities, bool isSoftDeleted = false)
        {
            if (!entities.SafeAny())
            {
                return;
            }

            foreach (var batch in entities.Batches(GeneralSettings.BatchSize))
            {
                await DeleteBatchAsync(batch, isSoftDeleted);
            }

        }

        public virtual async Task DeleteAsync(TEntity entity, bool isSoftDeleted = false)
        {
            if (isSoftDeleted)
            {
                entity.IsDeleted = true;
                UpdateEntity(entity);
            }
            else
            {
                DbSet.Remove(entity);
            }

            await DbContext.SaveChangesAsync();
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

        public virtual async Task<IList<TEntity>> SearchAllAsync(SearchCriteria criteria, params Expression<Func<TEntity, object>>[]? includes)
        {
            return await HandleSearchAllAsync(criteria, Query(includes));
        }

        public virtual async Task<IList<TEntity>> SearchAllAsync(SearchCriteria? criteria = null)
        {
            return await HandleSearchAllAsync(criteria ?? new SearchCriteria(), Includes());
        }

        public virtual async Task<IList<TDestination>> SearchAllWithProjectionASync<TDestination>(
            Expression<Func<TEntity, TDestination>> projection, SearchCriteria? criteria)
        {
            var result = new List<TDestination>();
            criteria ??= new SearchCriteria();
            criteria.Take = GeneralSettings.BatchSize;

            var query = BuildQuery(DbSet.AsQueryable(), criteria);
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
                var entities = await query.Select(projection).Skip(criteria.Skip).Take(criteria.Take).ToListAsync();
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

        public virtual async Task<GenericSearchResult<TEntity>> SearchAsync(SearchCriteria? criteria = null)
        {
            criteria ??= new SearchCriteria();
            var query = Includes();
            return await HandleSearchAsync(criteria, query);
        }

        public virtual async Task<GenericSearchResult<TEntity>> SearchAsync(SearchCriteria criteria, params Expression<Func<TEntity, object>>[]? includes)
        {
            return await HandleSearchAsync(criteria, Query(includes));
        }

        public virtual async Task<GenericSearchResult<TDestination>> SearchWithProjectionAsync<TDestination>(Expression<Func<TEntity, TDestination>> projection, SearchCriteria? criteria)
        {
            var result = new GenericSearchResult<TDestination>();
            criteria ??= new SearchCriteria();

            var query = Includes();
            query = BuildQuery(query, criteria);
            result.TotalCount = await query.CountAsync();
            if (result.TotalCount == 0)
            {
                return result;
            }

            query = BuildSort(query, criteria);

            result.Results = await query.Select(projection).Skip(criteria.Skip).Take(criteria.Take).ToListAsync();

            return result;

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

        protected void UpdateEntity(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        private async Task<GenericSearchResult<TEntity>> HandleSearchAsync(SearchCriteria criteria, IQueryable<TEntity> query)
        {
            query = BuildQuery(query, criteria);
            var result = new GenericSearchResult<TEntity>
            {
                TotalCount = await query.CountAsync()
            };
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

        private async Task AddBatchAsync(IList<TEntity> entities)
        {
            if (!entities.SafeAny())
            {
                return;
            }

            foreach (var entity in entities)
            {
                await DbSet.AddAsync(entity);
            }

            await DbContext.SaveChangesAsync();
        }

        private async Task CreateOrUpdateEntryAsync(TEntity entity)
        {
            if (entity.Id == 0)
            {
                await DbSet.AddAsync(entity);
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        private async Task AddOrUpdateBatchAsync(IList<TEntity> entities)
        {
            if (!entities.SafeAny())
            {
                return;
            }

            foreach (var entity in entities)
            {
                await CreateOrUpdateEntryAsync(entity);
            }

            await DbContext.SaveChangesAsync();
        }

        private async Task DeleteBatchAsync(IList<TEntity> entities, bool isSoftDeleted = false)
        {
            if (!entities.SafeAny())
            {
                return;
            }

            if (isSoftDeleted)
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                    UpdateEntity(entity);

                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    DbSet.Remove(entity);
                }
            }

            await DbContext.SaveChangesAsync();
        }
    }
}