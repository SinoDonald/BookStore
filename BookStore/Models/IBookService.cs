using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public interface IBookService
    {
        List<Book> GetAll();
        Book Get(int id);
        bool Insert(Book book);
        bool Update(Book book);
        bool Delete(int id);
        void Dispose();
    }
}