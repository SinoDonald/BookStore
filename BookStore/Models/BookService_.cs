using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models
{
    public class BookService_ : IBookService, IDisposable
    {
        private IBookRepository _repository;
        public BookService_()
        {
            //_repository = new BookDBRepository();
        }
        public BookService_(IBookRepository repository)
        {
            _repository = repository;
        }
        public List<Book> GetAll()
        {
            var books = _repository.GetAll();
            return books.Where(p => p.Price >= 50).ToList();
        }
        public Book Get(int id)
        {
            return _repository.Get(id);
        }
        public bool Insert(Book book)
        {
            if (book.Price < 50)
                book.Price = 50;
            return _repository.Insert(book);
        }
        public bool Update(Book book)
        {
            if (book.Price < 50)
                book.Price = 50;
            return _repository.Update(book);
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}