namespace EForm.Models
{
    public class PagingParameterModel
    {
        public int MaxPageSize { get; set; } = 1000;

        public int PageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 30;

        public int PageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}