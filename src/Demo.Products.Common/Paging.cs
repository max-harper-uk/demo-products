namespace Demo.Products.Common
{
    public struct PagedList<T>
    {
        public PagedList(IList<T> items, int skip, int take, int totalCount)
        {
            Items = items;
            Skip = skip;
            Take = take;
            TotalCount = totalCount;
        }

        public IList<T> Items { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public int TotalCount { get; set; }

        public readonly int Page => Skip == 0 ? 1 : Skip > TotalCount ? 1 : TotalCount - Skip + 1;

        public readonly int PageCount => TotalCount <= Take ? 1 
            : Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TotalCount - (Skip + Take)) / Take));
    }
}
