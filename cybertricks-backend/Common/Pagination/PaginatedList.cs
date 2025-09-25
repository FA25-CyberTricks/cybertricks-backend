using Microsoft.EntityFrameworkCore;

namespace ct.backend.Common.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source,
            int pageIndex,
            int pageSize,
            CancellationToken ct = default)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var count = await source.CountAsync(ct);
            var items = await source.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(ct);

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
