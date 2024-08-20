using Microsoft.AspNetCore.Mvc;
using shopping_books.Dto;
using shopping_books.Interfaces;
using shopping_books.Models;

namespace shopping_books.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                return BadRequest("Books list cannot be null or empty.");
            }

            var createdBooks = await _bookRepository.CreateAllBooksAsync(books);
            return Ok(createdBooks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredBooks([FromBody] BookFilterRequestDto filterRequest)
        {
            if (filterRequest == null)
            {
                return BadRequest("Filter request cannot be null.");
            }

            var filteredBooks = await _bookRepository.GetFilteredBooksAsync(filterRequest);
            return Ok(filteredBooks);
        }
    }
}