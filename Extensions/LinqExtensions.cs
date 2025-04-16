namespace SmartInventoryBE.Extensions
{
    public static class LinqExtensions
    {
        public static bool SafeAny<T>(this IEnumerable<T>? items)
        {
            return items != null && items.Any();
        }

        public static bool SafeAny<T>(this IEnumerable<T>? items, Func<T, bool> predicate)
        {
            return items != null && items.Any(predicate);
        }

        public static T? SafeFirstOrDefault<T>(this IEnumerable<T?> items) where T : class
        {
            return items?.FirstOrDefault();
        }

        public static T? SafeFirstOrDefault<T>(this IEnumerable<T?> items, Func<T, bool> predicate) where T : class
        {
            return items?.FirstOrDefault(predicate);
        }

        public static IEnumerable<T> SafeWhere<T>(this IEnumerable<T>? items, Func<T, bool> predicate)
        {
            return items == null ? [] : items.Where(predicate);
        }

        public static bool SafeContains<T>(this IEnumerable<T>? source, T value)
        {
            return source != null && source.Contains(value);
        }

        public static bool SafeContains<T>(this IEnumerable<T>? source, T value, IEqualityComparer<T> comparer)
        {
            return source != null && source.Contains(value, comparer);
        }

        public static int SafeCount<T>(this IEnumerable<T>? source)
        {
            return source?.Count() ?? 0;
        }

        public static int SafeCount<T>(this IEnumerable<T>? source, Func<T, bool> predicate)
        {
            
            return source?.Count(predicate) ?? 0;
        }

        public static IEnumerable<T> SafeForeach<T>(this IEnumerable<T> source)
        {
            return source ?? [];
        }

        public static IList<T> SafeToList<T>(this IEnumerable<T>? source)
        {
            return source == null ? [] : source.ToList();
        }

        public static int SafeSum<T>(this IEnumerable<T>? source, Func<T, int> selector)
        {
            return source?.Sum(selector) ?? 0;
        }

        public static decimal SafeSum<T>(this IEnumerable<T>? source, Func<T, decimal> selector)
        {
            return source?.Sum(selector) ?? 0;
        }

        public static int SafeMaxCount<T>(this IEnumerable<IEnumerable<T>> list)
        {
            var enumerable = list.ToList();
            return !enumerable.SafeAny() ? 0 : enumerable.Select(x => x.Count()).Max();
        }

        public static void SafeAdd<T>(this IList<T>? list, T item)
        {
            if (list == null || item == null)
            {
                return;
            }

            list.Add(item);
        }

        public static IEnumerable<IList<T>> Batches<T>(this IEnumerable<T> records, int batchSize)
        {
            var result = new List<T>(batchSize);

            foreach (var record in records)
            {
                result.Add(record);

                if (result.Count < batchSize) continue;
                yield return result;

                result = new List<T>(batchSize);
            }

            if (result.Any())
            {
                yield return result;
            }
        }

        public static IEnumerable<IndexedItem<T>> WithIndex<T>(this IEnumerable<T> source)
        {
            var index = 0;
            foreach (var item in source)
            {
                yield return new IndexedItem<T>(item, index++);
            }
        }

        public readonly struct IndexedItem<T>(T item, int index)
        {
            public T Item { get; } = item;
            public int Index { get; } = index;
        }
    }
}
