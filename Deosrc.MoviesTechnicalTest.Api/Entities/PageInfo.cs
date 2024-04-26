namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record PageInfo
    {
        public PageInfo(PagingOptions pagingOptions, bool hasMorePages)
        {
            ItemsPerPage = pagingOptions.ItemsPerPage;
            PageNumber = pagingOptions.Page;
            HasMorePages = hasMorePages;
        }

        public int ItemsPerPage { get; }
        public int PageNumber { get; }
        public bool HasMorePages { get; }
    }
}
