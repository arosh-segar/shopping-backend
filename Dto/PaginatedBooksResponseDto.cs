using shopping_books.Models;

namespace shopping_books.Dto
{
    public class PaginatedBooksResponseDto
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}