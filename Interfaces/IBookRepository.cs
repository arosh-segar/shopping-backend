using shopping_books.Dto;
using shopping_books.Models;

namespace shopping_books.Interfaces
{
    public interface IBookRepository
    {
        Task<PaginatedBooksResponseDto> GetFilteredBooksAsync(BookFilterRequestDto filterRequest);

        Task<List<Book>> CreateAllBooksAsync(List<Book> Books);

        Task<Book?> GetByIdAsync(int id);
    }
}