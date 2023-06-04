namespace TimeSheet_Backend.Models.DTOs
{
    public class RequestParams
    {
        const int maxPageSize = 100;
        private int _pageSize;

        public int PageNumber { get; set; } = 1;
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
