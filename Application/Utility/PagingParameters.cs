namespace eStore_Admin.Application.Utility
{
    public class PagingParameters
    {
        private const int MaxPageSize = 100;
        
        private int _pageNumber;
        private int _pageSize;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value > 0 ? value : 1;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < MaxPageSize ? value : MaxPageSize;
        }

        public PagingParameters()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PagingParameters(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}