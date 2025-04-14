using System.Linq.Expressions;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Models;
using SmartInventoryBE.ProjectAggregate.Search;

namespace SmartInventoryBE.Interfaces.RepositoryInterfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Asynchronously retrieves all entities matching the specified search criteria, 
    /// with optional related data to include in the query.
    /// </summary>
    /// <param name="criteria">The search criteria used to filter the entities.</param>
    /// <param name="includes">An optional array of expressions specifying related data to include.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a list of entities
    /// that match the search criteria.
    /// </returns>
    Task<IList<TEntity>> SearchAllAsync(SearchCriteria criteria, params Expression<Func<TEntity, object>>[]? includes);
    /// <summary>
    /// Asynchronously retrieves all entities matching the specified search criteria, 
    /// with an option to use default criteria if none is provided.
    /// </summary>
    /// <param name="criteria">
    /// The optional search criteria used to filter the entities. 
    /// If null, a default <see cref="SearchCriteria"/> will be used.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of entities that match the criteria.
    /// </returns>
    Task<IList<TEntity>> SearchAllAsync(SearchCriteria? criteria = null);
    /// <summary>
    /// Asynchronously retrieves all entities matching the specified search criteria, 
    /// projecting the results into a specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type to which the entities are projected.</typeparam>
    /// <param name="projection">
    /// An expression that defines the projection from the entity type <typeparamref name="TEntity"/> 
    /// to the destination type <typeparamref name="TDestination"/>.
    /// </param>
    /// <param name="criteria">
    /// The optional search criteria used to filter the entities. 
    /// If null, a default <see cref="SearchCriteria"/> will be used with batching enabled.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a list of projected entities of type <typeparamref name="TDestination"/> that match the criteria.
    /// </returns>
    Task<IList<TDestination>> SearchAllWithProjectionASync<TDestination>(
        Expression<Func<TEntity, TDestination>> projection, SearchCriteria? criteria = null);

    /// <summary>
    /// Asynchronously counts the number of entities matching the specified search criteria.
    /// </summary>
    /// <param name="criteria">
    /// The search criteria used to filter the entities.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the count of entities that match the criteria.
    /// </returns>
    Task<int> CountAsync(SearchCriteria criteria);
    /// <summary>
    /// Asynchronously counts the number of entities that match the specified criteria expression.
    /// </summary>
    /// <param name="criteria">
    /// An expression used to filter the entities. Only entities that match the expression will be counted.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the count of entities that match the criteria.
    /// </returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria);

    /// <summary>
    /// Asynchronously retrieves a paginated result of entities matching the specified search criteria.
    /// </summary>
    /// <param name="criteria">
    /// The optional search criteria used to filter and paginate the entities. 
    /// If null, a default <see cref="SearchCriteria"/> will be used.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a 
    /// <see cref="GenericSearchResult{T}"/> with the entities that match the criteria.
    /// </returns>
    Task<GenericSearchResult<TEntity>> SearchAsync(SearchCriteria? criteria = null);

    /// <summary>
    /// Asynchronously retrieves a paginated result of entities matching the specified search criteria, 
    /// with optional related data to include in the query.
    /// </summary>
    /// <param name="criteria">
    /// The search criteria used to filter and paginate the entities.
    /// </param>
    /// <param name="includes">
    /// An optional array of expressions specifying related data to include in the query.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a 
    /// <see cref="GenericSearchResult{TEntity}"/> with the entities that match the criteria.
    /// </returns>
    Task<GenericSearchResult<TEntity>> SearchAsync(SearchCriteria criteria,
        params Expression<Func<TEntity, object>>[]? includes);

    /// <summary>
    /// Asynchronously retrieves a paginated result of entities, projecting the results into a specified destination type,
    /// based on the specified search criteria.
    /// </summary>
    /// <typeparam name="TDestination">The type to which the entities are projected.</typeparam>
    /// <param name="projection">
    /// An expression that defines the projection from the entity type <typeparamref name="TEntity"/> 
    /// to the destination type <typeparamref name="TDestination"/>.
    /// </param>
    /// <param name="criteria">
    /// The optional search criteria used to filter and paginate the entities. 
    /// If null, a default <see cref="SearchCriteria"/> will be used.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a 
    /// <see cref="GenericSearchResult{TDestination}"/> with the projected entities that match the criteria.
    /// </returns>
    Task<GenericSearchResult<TDestination>> SearchWithProjectionAsync<TDestination>(
        Expression<Func<TEntity, TDestination>> projection, SearchCriteria? criteria = null);

    /// <summary>
    /// Asynchronously retrieves a single entity that matches the specified criteria expression.
    /// </summary>
    /// <param name="criteria">
    /// An expression used to filter the entities. The first entity that matches the expression will be retrieved.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the first entity that matches the criteria, or null if no entity is found.
    /// </returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> criteria);

    /// <summary>
    /// Asynchronously retrieves a single entity that matches the specified criteria expression, 
    /// with optional related data to include in the query.
    /// </summary>
    /// <param name="criteria">
    /// An expression used to filter the entities. The first entity that matches the expression will be retrieved.
    /// </param>
    /// <param name="includes">
    /// An optional array of expressions specifying related data to include in the query.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the first entity that matches the criteria, 
    /// or null if no entity is found.
    /// </returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> criteria,
        params Expression<Func<TEntity, object>>[]? includes);
    /// <summary>
    /// Asynchronously retrieves a single entity that matches the specified criteria expression, 
    /// projecting the result into a specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type to which the entity is projected.</typeparam>
    /// <param name="criteria">
    /// An expression used to filter the entities. The first entity that matches the expression will be retrieved.
    /// </param>
    /// <param name="projection">
    /// An expression that defines the projection from the entity type <typeparamref name="TEntity"/> 
    /// to the destination type <typeparamref name="TDestination"/>.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the first projected entity that matches the criteria, 
    /// or null if no entity is found.
    /// </returns>
    Task<TDestination?> GetWithProjectionAsync<TDestination>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TDestination>> projection);

    /// <summary>
    /// Asynchronously retrieves a single entity by its unique identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the entity to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the entity with the specified ID, 
    /// or null if no entity is found.
    /// </returns>
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[]? includes);

    /// <summary>
    /// Asynchronously retrieves a list of entities by their unique identifiers, processing the IDs in batches.
    /// </summary>
    /// <param name="ids">
    /// A list of unique identifiers of the entities to retrieve.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of entities with the specified IDs.
    /// </returns>
    Task<IList<TEntity>> GetByIdsAsync(IList<int> ids);
    /// <summary>
    /// Asynchronously retrieves a list of entities by their unique identifiers, processing the IDs in batches, 
    /// with optional related data to include in the query.
    /// </summary>
    /// <param name="ids">
    /// A list of unique identifiers of the entities to retrieve.
    /// </param>
    /// <param name="includes">
    /// An optional array of expressions specifying related data to include in the query.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of entities with the specified IDs.
    /// </returns>
    Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, params Expression<Func<TEntity, object>>[]? includes);
    /// <summary>
    /// Asynchronously retrieves a list of projected entities by their unique identifiers, 
    /// processing the IDs in batches and projecting the results into a specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type to which the entities are projected.</typeparam>
    /// <param name="ids">
    /// A list of unique identifiers of the entities to retrieve.
    /// </param>
    /// <param name="projection">
    /// An expression that defines the projection from the entity type <typeparamref name="TEntity"/> 
    /// to the destination type <typeparamref name="TDestination"/>.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of projected entities 
    /// with the specified IDs.
    /// </returns>
    Task<IList<TDestination>> GetByIdsProjectionAsync<TDestination>(IList<int> ids, Expression<Func<TEntity, TDestination>> projection);

    /// <summary>
    /// Asynchronously retrieves a list of entities that match the specified criteria expression, 
    /// with optional related data to include in the query.
    /// </summary>
    /// <param name="criteria">
    /// An expression used to filter the entities. Only entities that match the expression will be retrieved.
    /// </param>
    /// <param name="includes">
    /// An optional array of expressions specifying related data to include in the query.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of entities that match the criteria.
    /// </returns>
    Task<IList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[]? includes);
    /// <summary>
    /// Asynchronously retrieves a list of entities that match the specified criteria expression, 
    /// with default related data included in the query.
    /// </summary>
    /// <param name="criteria">
    /// An expression used to filter the entities. Only entities that match the expression will be retrieved.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of entities that match the criteria.
    /// </returns>
    Task<IList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> criteria);
    /// <summary>
    /// Asynchronously retrieves a list of projected entities that match the specified criteria expression, 
    /// projecting the results into a specified destination type.
    /// </summary>
    /// <typeparam name="TDestination">The type to which the entities are projected.</typeparam>
    /// <param name="criteria">
    /// An expression used to filter the entities. Only entities that match the expression will be retrieved.
    /// </param>
    /// <param name="projection">
    /// An expression that defines the projection from the entity type <typeparamref name="TEntity"/> 
    /// to the destination type <typeparamref name="TDestination"/>.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of projected entities 
    /// that match the criteria.
    /// </returns>
    Task<IList<TDestination>> ListWithProjectionAsync<TDestination>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TDestination>> projection);

    /// <summary>
    /// Asynchronously adds or updates an entity in the database, 
    /// saving changes to the context after the operation.
    /// </summary>
    /// <param name="entity">
    /// The entity to add or update in the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task AddOrUpdateAsync(TEntity entity);
    /// <summary>
    /// Asynchronously adds or updates a list of entities in the database, processing them in batches 
    /// and saving changes after each batch.
    /// </summary>
    /// <param name="entities">
    /// A list of entities to add or update in the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task AddOrUpdateAsync(IList<TEntity> entities);

    /// <summary>
    /// Asynchronously updates a list of entities in the database, processing them in batches.
    /// </summary>
    /// <param name="entities">
    /// A list of entities to update in the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task UpdateAsync(IList<TEntity> entities);
    /// <summary>
    /// Asynchronously updates a single entity in the database and saves the changes.
    /// </summary>
    /// <param name="entity">
    /// The entity to update in the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Asynchronously adds a list of entities to the database, processing them in batches.
    /// </summary>
    /// <param name="entities">
    /// A list of entities to add to the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task AddAsync(IList<TEntity> entities);
    /// <summary>
    /// Asynchronously adds a single entity to the database and saves the changes.
    /// </summary>
    /// <param name="entity">
    /// The entity to add to the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Asynchronously deletes entities that match the specified criteria. 
    /// Supports both soft and hard deletes, depending on the <paramref name="isSoftDeleted"/> flag.
    /// </summary>
    /// <param name="criteria">
    /// An expression used to filter the entities to delete.
    /// </param>
    /// <param name="isSoftDeleted">
    /// A flag indicating whether the deletion should be a soft delete (marking the entity as deleted) or a hard delete (removing the entity).
    /// Default is false for a hard delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> criteria, bool isSoftDeleted = false);
    /// <summary>
    /// Asynchronously deletes an entity by its unique identifier. 
    /// Supports both soft and hard deletes, depending on the <paramref name="isSoftDeleted"/> flag.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the entity to delete.
    /// </param>
    /// <param name="isSoftDeleted">
    /// A flag indicating whether the deletion should be a soft delete (marking the entity as deleted) or a hard delete (removing the entity).
    /// Default is false for a hard delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task DeleteAsync(int id, bool isSoftDeleted = false);
    /// <summary>
    /// Asynchronously deletes entities by their unique identifiers. 
    /// Supports both soft and hard deletes, depending on the <paramref name="isSoftDeleted"/> flag.
    /// </summary>
    /// <param name="ids">
    /// A list of unique identifiers of the entities to delete.
    /// </param>
    /// <param name="isSoftDeleted">
    /// A flag indicating whether the deletion should be a soft delete (marking the entities as deleted) or a hard delete (removing the entities).
    /// Default is false for a hard delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task DeleteAsync(IList<int> ids, bool isSoftDeleted = false);
    /// <summary>
    /// Asynchronously deletes a list of entities. 
    /// Supports both soft and hard deletes, depending on the <paramref name="isSoftDeleted"/> flag.
    /// The entities are processed in batches for efficiency.
    /// </summary>
    /// <param name="entities">
    /// A list of entities to delete.
    /// </param>
    /// <param name="isSoftDeleted">
    /// A flag indicating whether the deletion should be a soft delete (marking the entities as deleted) or a hard delete (removing the entities).
    /// Default is false for a hard delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task DeleteAsync(IList<TEntity> entities, bool isSoftDeleted = false);
    /// <summary>
    /// Asynchronously deletes a single entity. 
    /// Supports both soft and hard deletes, depending on the <paramref name="isSoftDeleted"/> flag.
    /// </summary>
    /// <param name="entity">
    /// The entity to delete.
    /// </param>
    /// <param name="isSoftDeleted">
    /// A flag indicating whether the deletion should be a soft delete (marking the entity as deleted) or a hard delete (removing the entity).
    /// Default is false for a hard delete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task DeleteAsync(TEntity entity, bool isSoftDeleted = false);
}