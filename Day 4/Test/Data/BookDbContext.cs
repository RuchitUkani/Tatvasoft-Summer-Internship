using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Role> Roles { get; set; }
        public DbSet<Models.Book> Books { get; set; }

    }
}
