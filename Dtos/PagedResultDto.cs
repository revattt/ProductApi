using NPOI.SS.Formula.Functions;

namespace ProductApi.Dtos
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages =>
        (int)Math.Ceiling((double)TotalCount / PageSize);

    }
}
