using Microsoft.EntityFrameworkCore;
using SmartInventoryBE.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace SmartInventoryBE.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeIgnoreDeleted<T>(this IQueryable<T> source, string path) where T : BaseEntity
        {
            if (!string.IsNullOrEmpty(path))
            {
                return source.Include(path)
                    .Where(a => !a.IsDeleted);
            }

            return source;
        }
        public static IQueryable<T> IncludeFilteredNavigation<T>(this IQueryable<T> query, IList<string> navigationPropertyPaths = null) where T : BaseEntity
        {
            try
            {
                if (navigationPropertyPaths != null)
                {
                    foreach (string path in navigationPropertyPaths)
                    {
                        query = query.Include(path).IncludeFilteredNavigation(path);
                    }
                }
            }
            catch (Exception ex)
            {
                //If an exception was throw, we will to return default query
                Console.WriteLine(ex);
            }

            return query;
        }

        public static IQueryable<T> IncludeFilteredNavigation<T>(
        this IQueryable<T> query,
        string navigationProperty) where T : BaseEntity
        {
            if (string.IsNullOrWhiteSpace(navigationProperty))
                throw new ArgumentException("Navigation property cannot be null or empty.", nameof(navigationProperty));

            var entityType = typeof(T);
            var navigationPropertyInfo = entityType.GetProperty(navigationProperty);
            if (navigationPropertyInfo == null)
                throw new ArgumentException($"Property '{navigationProperty}' not found on type '{entityType.Name}'.", nameof(navigationProperty));

            var navigationType = navigationPropertyInfo.PropertyType;
            var parameter = Expression.Parameter(entityType, "x");
            var navigation = Expression.Property(parameter, navigationProperty);

            if (typeof(IEnumerable<BaseEntity>).IsAssignableFrom(navigationType))
            {
                // Handle collections (e.g., related entities collection)
                return ApplyCollectionFilter(query, parameter, navigation, navigationPropertyInfo, navigationType);
            }
            else if (typeof(BaseEntity).IsAssignableFrom(navigationType))
            {
                // Handle single entity navigation properties (e.g., a related entity with IsDeleted)
                return ApplySingleEntityFilter(query, parameter, navigation);
            }

            throw new InvalidOperationException($"Navigation property '{navigationProperty}' must be a collection or a single entity of type BaseEntity.");
        }

        private static IQueryable<T> ApplyCollectionFilter<T>(
            IQueryable<T> query,
            ParameterExpression parameter,
            Expression navigation,
            PropertyInfo navigationPropertyInfo,
            Type navigationType) where T : BaseEntity
        {
            var navigationItemType = navigationType.GetGenericArguments().FirstOrDefault();
            if (navigationItemType == null || !typeof(BaseEntity).IsAssignableFrom(navigationItemType))
                throw new InvalidOperationException($"Navigation property '{navigationPropertyInfo.Name}' is not a valid collection of BaseEntity.");

            var navParameter = Expression.Parameter(navigationItemType, "nav");
            var navCondition = Expression.Equal(
                Expression.Property(navParameter, nameof(BaseEntity.IsDeleted)),
                Expression.Constant(false));
            var navLambda = Expression.Lambda(navCondition, navParameter);

            // Apply filtering on the navigation collection
            var whereMethod = typeof(Enumerable).GetMethods()
                .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
                .MakeGenericMethod(navigationItemType);

            var filteredNavigation = Expression.Call(whereMethod, navigation, navLambda);
            var castFilteredNavigation = Expression.Convert(filteredNavigation, navigationType);

            // Create a new projection including the filtered navigation
            var bindings = CreateMemberBindings<T>(parameter, navigationPropertyInfo, castFilteredNavigation);
            var projection = Expression.MemberInit(Expression.New(typeof(T)), bindings);
            var selector = Expression.Lambda<Func<T, T>>(projection, parameter);

            return query.Select(selector);
        }

        private static IEnumerable<MemberBinding> CreateMemberBindings<T>(
            ParameterExpression parameter,
            PropertyInfo filteredProperty,
            Expression filteredNavigation) where T : BaseEntity
        {
            var entityType = typeof(T);
            var bindings = new List<MemberBinding>
            {
                Expression.Bind(filteredProperty, filteredNavigation)
            };

            // Include all other properties of the entity
            bindings.AddRange(entityType.GetProperties()
                .Where(p => p != filteredProperty)
                .Select(p => Expression.Bind(p, Expression.Property(parameter, p))));

            return bindings;
        }

        private static IQueryable<T> ApplySingleEntityFilter<T>(
            IQueryable<T> query,
            ParameterExpression parameter,
            Expression navigation) where T : BaseEntity
        {
            var isDeletedCondition = Expression.Equal(
                Expression.PropertyOrField(navigation, nameof(BaseEntity.IsDeleted)),
                Expression.Constant(false));
            var lambda = Expression.Lambda<Func<T, bool>>(isDeletedCondition, parameter);

            return query.Where(lambda);
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, Dictionary<string, bool> sortValue = null) where T : BaseEntity
        {
            if (!sortValue.SafeAny())
            {
                return query;
            }

            foreach (var sortCriterion in sortValue)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, sortCriterion.Key);
                var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

                query = sortCriterion.Value ? query.OrderByDescending(lambda) : query.OrderBy(lambda);
            }
            return query;
        }

    }
}
