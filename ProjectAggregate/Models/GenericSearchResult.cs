namespace SmartInventoryBE.ProjectAggregate.Models
{
    public class GenericSearchResult<T>
    {

        public int TotalCount { get; set; }
        public IList<T> Results { get; set; }

        public GenericSearchResult()
        {
            Results = new List<T>();
        }

        public GenericSearchResult(List<T> results)
        {
            Results = results;
            TotalCount = results.Count;
        }

        public GenericSearchResult(List<T> results, int totalCount)
        {
            Results = results;
            TotalCount = totalCount;
        }
    }
}
