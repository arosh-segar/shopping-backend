using Microsoft.EntityFrameworkCore;
using shopping_books.Models;

namespace shopping_books.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Book> Book { get; set; }
    }
}