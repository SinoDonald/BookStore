using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace BookStore.Models
{
    public class BookDBRepository : IBookRepository, IDisposable
    {
        private string _bookConnection = ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;
        private SqlConnection conn;
        public BookDBRepository()
        {
            conn = new SqlConnection(_bookConnection);
        }
        public List<Book> GetAll()
        {
            string sql = @"select * from Book order by id";
            List<Book> ret = conn.Query<Book>(sql).ToList();

            return ret;
        }
        public Book Get(int id)
        {
            string sql = @"select * from Book where id=@id";
            Book ret = conn.Query<Book>(sql, new { id }).SingleOrDefault();

            return ret;
        }
        public bool Insert(Book book)
        {
            string sql = @"Insert into Book (id, author, price) values (@id, @author, @price)";
            int ret = conn.Execute(sql, book);

            return ret > 0 ? true : false;
        }
        public bool Update(Book book)
        {
            string sql = @"Update Book Set author=@author, price=@price where id=@id";
            int ret = conn.Execute(sql, book);

            return ret > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            string sql = @"delete Book where id=@id";
            int ret = conn.Execute(sql, new { id });

            return ret > 0 ? true : false;
        }
        public void Dispose()
        {
            return;
        }
    }
}