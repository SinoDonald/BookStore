using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models
{
    public class BookService : IDisposable
    {
        private DBUnitOfWork _dbContext;
        public BookService()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Prj\ASP.Net\BookStore\BookStore\App_Data\Database1.mdf;Integrated Security=True";
            _dbContext = new DBUnitOfWork(connectionString);
        }
        public List<Book> GetAll()
        {
            var books = _dbContext.Book.GetAll();
            return books.Where(p => p.Price >= 50).ToList();
        }
        public Book Get(int id)
        {
            return _dbContext.Book.Get(id);
        }
        public bool Insert(Book book)
        {
            if (book.Price < 50)
                book.Price = 50;
            var ret = _dbContext.Book.Insert(book);
            _dbContext.Commit();
            return ret;
        }
        public bool Update(Book book)
        {
            if (book.Price < 50)
                book.Price = 50;
            var ret = _dbContext.Book.Update(book);
            _dbContext.Commit();
            return ret;
        }
        public bool Delete(int id)
        {
            var ret = _dbContext.Book.Delete(id);
            _dbContext.Commit();
            return ret;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}