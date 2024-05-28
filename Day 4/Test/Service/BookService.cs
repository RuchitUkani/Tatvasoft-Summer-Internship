using System.Collections.Generic;
using System.Linq;
using Test.Data;
using Test.Models;

namespace Test.Service
{
    public class BookService
    {
        private readonly BookDbContext _context;

        public BookService(BookDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll() => _context.Books.ToList();

        public Book GetById(int id) => _context.Books.FirstOrDefault(b => b.Id == id);

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}