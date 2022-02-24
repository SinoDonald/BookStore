using System.Collections.Generic;

namespace BookStore.Models
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book Get(int id);
        bool Insert(Book book);
        bool Update(Book book);
        bool Delete(int id);
        void Dispose();
    }
}