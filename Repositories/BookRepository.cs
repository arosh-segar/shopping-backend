using Microsoft.EntityFrameworkCore;
using shopping_books.Data;
using shopping_books.Dto;
using shopping_books.Interfaces;
using shopping_books.Models;

namespace shopping_books.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> CreateAllBooksAsync(List<Book> books)
        {
            await _context.Book.AddRangeAsync(books);

            await _context.SaveChangesAsync();

            return books;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Book.Where(b => b.Id == id).FirstOrDefaultAsync();

        }

        public async Task<PaginatedBooksResponseDto> GetFilteredBooksAsync(BookFilterRequestDto filterRequest)
        {
            var query = _context.Book.AsQueryable();

            // Apply filters
            if (filterRequest.Categories != null && filterRequest.Categories.Length > 0)
            {
                var categories = filterRequest.Categories.Select(c => c.ToLower()).ToArray();
                query = query.Where(b => categories.Contains(b.Category.ToLower()));
            }

            if (filterRequest.MinRating.HasValue)
            {
                query = query.Where(b => b.Rating >= filterRequest.MinRating.Value);
            }

            if (filterRequest.MaxRating.HasValue)
            {
                query = query.Where(b => b.Rating <= filterRequest.MaxRating.Value);
            }

            if (filterRequest.MinPrice.HasValue)
            {
                query = query.Where(b => b.Price >= filterRequest.MinPrice.Value);
            }

            if (filterRequest.MaxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= filterRequest.MaxPrice.Value);
            }

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((filterRequest.PageNumber - 1) * filterRequest.PageSize)
                .Take(filterRequest.PageSize)
                .Select(b => new Book
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author,
                    Category = b.Category,
                    Price = b.Price,
                    Rating = b.Rating,
                    ImgUrl = b.ImgUrl,
                    Quantity = b.Quantity
                })
                .ToListAsync();

            var response = new PaginatedBooksResponseDto
            {
                TotalCount = totalCount,
                PageNumber = filterRequest.PageNumber,
                PageSize = filterRequest.PageSize,
                Books = books
            };

            return response;
        }
    }
}