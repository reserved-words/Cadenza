namespace Cadenza.Library
{
    internal static class Extensions
    {
        public static ListResponse<TResult> ToListResponse<T, TResult>(this IEnumerable<T> allItems, Func<T, object> order, int page, int limit) where T : TResult
        {
            var list = allItems.ToList();

            var skip = (page - 1) * limit;

            var items = list
                .OrderBy(i => order(i))
                .Skip(skip)
                .Take(limit)
                .OfType<TResult>()
                .ToList();

            var total = list.Count;

            return new ListResponse<TResult>
            {
                Items = items,
                Limit = limit,
                Page = page,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / limit)
            };
        }

        public static ListResponse<T> ToListResponse<T>(this IEnumerable<T> allItems, Func<T, object> order, int page, int limit) 
        {
            var list = allItems.ToList();

            var skip = (page - 1) * limit;

            var items = list
                .OrderBy(i => order(i))
                .Skip(skip)
                .Take(limit)
                .ToList();

            var total = list.Count;

            return new ListResponse<T>
            {
                Items = items,
                Limit = limit,
                Page = page,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / limit)
            };
        }

    }
}
