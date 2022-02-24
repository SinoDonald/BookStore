using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookTxtRepository : IBookRepository, IDisposable
    {
        private string _appData = "";
        public BookTxtRepository()
        {
            _appData = HttpContext.Current.Server.MapPath("~/APP_Data");
        }
        public List<Book> GetAll()
        {
            var bookList = System.IO.Directory.GetFiles(_appData, "*.txt").OrderBy(p =>
            System.IO.Path.GetFileName(p)).ToList();
            List<Book> books = new List<Book>();
            foreach (var item in bookList)
            {
                string[] bookText = System.IO.File.ReadAllLines(item);
                Book book = new Book();
                book.ID = Convert.ToInt32(bookText[0]);
                book.Author = bookText[1];
                book.Price = Convert.ToInt32(bookText[2]);
                books.Add(book);
            }
            return books;
        }
        public Book Get(int id)
        {
            string fn = Path.Combine(_appData, id.ToString() + ".txt");
            string[] bookText = File.ReadAllLines(fn);
            Book book = new Book();
            book.ID = Convert.ToInt32(bookText[0]);
            book.Author = bookText[1];
            book.Price = Convert.ToInt32(bookText[2]);
            return book;
        }
        public bool Insert(Book book)
        {
            return Update(book);
        }
        public bool Update(Book book)
        {
            string fn = Path.Combine(_appData, book.ID.ToString() + ".txt");
            string[] bookText = new string[3];
            bookText[0] = book.ID.ToString();
            bookText[1] = book.Author;
            bookText[2] = book.Price.ToString();
            bool ret = false;
            try
            {
                System.IO.File.WriteAllLines(fn, bookText);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
        public bool Delete(int id)
        {
            string fn = Path.Combine(_appData, id.ToString() + ".txt");
            bool ret = false;
            if (!System.IO.File.Exists(fn)) return ret;
            try
            {
                System.IO.File.Delete(fn);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
        public void Dispose()
        {
            return;
        }
    }
}