namespace shopping_books.Dto
{
    public class BookFilterRequestDto
    {
        public string[]? Categories { get; set; }
        public double? MinRating { get; set; }
        public double? MaxRating { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}