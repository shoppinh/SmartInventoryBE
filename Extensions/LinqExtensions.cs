namespace SmartInventoryBE.Extensions
{
    public static class LinqExtensions
    {
        public static bool SafeAny<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                return false;
            }

            return items.Any();
        }
    }
}
