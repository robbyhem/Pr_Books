using Books.DataAccess.Data;
using Books.DataAccess.Repository.IRepository;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Book book)
        {
            _context.Bookss.Update(book);

            //var bookDb = _context.Bookss.FirstOrDefault(x => x.Id == book.Id);
            //if (bookDb != null)
            //{
            //    bookDb.Title = book.Title;
            //    bookDb.Author = book.Author;
            //    bookDb.Description = book.Description;
            //    bookDb.ISBN = book.ISBN;
            //    bookDb.ListPrice = book.ListPrice;
            //    bookDb.Price = book.Price;
            //    bookDb.Price50 = book.Price50;
            //    bookDb.Price100 = book.Price100;
            //    bookDb.CategoryId = book.CategoryId;
            //    if (bookDb.ImageUrl != null)
            //    {
            //        book.ImageUrl = book.ImageUrl;
            //    }
            //}
        }
    }
}
