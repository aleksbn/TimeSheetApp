namespace TimeSheet_Backend.Models.DTOs
{
    public class RequestParams
    {
        const int maxPageSize = 100;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 0;
        public int PageSize { 
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
