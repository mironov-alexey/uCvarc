using System.Collections.Generic;

namespace TheBeachBots
{
    public class IndexContainer<T>
    {
        public int Index { get; private set; }
        public T Item { get; private set; }

        public IndexContainer(int index, T item)
        {
            Index = index;
            Item = item;
        }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<IndexContainer<T>> Enumerate<T>(this IEnumerable<T> source)
        {
            var index = 0;
            foreach (var e in source)
                yield return new IndexContainer<T>(index++, e);
        }
    }
}
