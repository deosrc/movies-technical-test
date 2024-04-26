namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record PageInfo
    {
        public PageInfo()
        {
            // Nothing to do.
        }

        public PageInfo(PagingOptions pagingOptions, bool hasMorePages)
        {
            ItemsPerPage = pagingOptions.ItemsPerPage;
            PageNumber = pagingOptions.Page;
            HasMorePages = hasMorePages;
        }

        public int ItemsPerPage { get; init; }
        public int PageNumber { get; init; }
        public bool HasMorePages { get; init; }
    }
}
