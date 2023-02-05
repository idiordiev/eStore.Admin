namespace eStore_Admin.Application.Utility;

public class PagingParameters
{
    private const int MaxPageSize = 100;

    private const int DefaultPageSize = 10;
    private const int DefaultPageNumber = 1;

    private int _pageNumber;
    private int _pageSize;

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

    public int PageNumber
    {
        get
        {
            return _pageNumber;
        }
        set
        {
            _pageNumber = value < 1 ? DefaultPageNumber : value;
        }
    }

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            if (value < 1)
            {
                _pageSize = DefaultPageSize;
            }
            else if (value > MaxPageSize)
            {
                _pageSize = MaxPageSize;
            }
            else
            {
                _pageSize = value;
            }
        }
    }
}