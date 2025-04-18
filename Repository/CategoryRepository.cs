using Microsoft.Extensions.Options;
using SmartInventoryBE.Interfaces.RepositoryInterfaces;
using SmartInventoryBE.Models;
using SmartInventoryBE.ProjectAggregate.Search;
using SmartInventoryBE.Settings;

namespace SmartInventoryBE.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IOptions<GeneralSettings> generalSettingsOptions, SmartInventoryContext context) : base(generalSettingsOptions, context)
        {
        }

        protected override IQueryable<Category> BuildQuery(IQueryable<Category> query, SearchCriteria searhCriteria)
        {
            if (searhCriteria is not CategorySearchCriteria criteria)
            {
                return base.BuildQuery(query, searhCriteria);

            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(x => x.Name.Contains(criteria.Name));
            }

            return query;

        }
    }
}
