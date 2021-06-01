namespace Musicalog.Shared.Models
{
    public class FilterParameters
    {
        public int MaxPageSize { get; set; } = 50;
        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = (value > MaxPages) ? MaxPages : value;
        }

        private int pageSize = 10;
        private int pageNumber = 1;
        public int MaxPages => (MaxPageSize % PageSize == 0) ? (MaxPageSize / PageSize) : (MaxPageSize / PageSize) + 1;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public bool OrderByArtist { get; set; }
        public bool OrderByLabel { get; set; }
        public bool OrderByStockCount { get; set; }
    }
}
