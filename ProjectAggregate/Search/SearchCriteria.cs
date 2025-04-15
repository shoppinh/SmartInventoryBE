namespace SmartInventoryBE.ProjectAggregate.Search
{
    public class SearchCriteria
    {
        public virtual int? PageNumber { get; set; }
        public virtual int Skip { get; set; }
        public virtual int Take { get; set; }
        public virtual IList<SortCriteria> SortCriteria { get; set; } = new List<SortCriteria>();

    }
    public class SortCriteria
    {
        public virtual string SortBy { get; set; }
        public virtual bool SortDescending { get; set; } = false;
    }
}
