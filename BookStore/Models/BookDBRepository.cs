﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BookStore.Models
{
    public class BookDBRepository : IDisposable
    {
        private IDbTransaction Transaction { get; set; }
        private IDbConnection Connection { get { return Transaction.Connection; } }
        public BookDBRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
        public IEnumerable<Book> GetAll()
        {
            string sql = @"select * from Book order by id";
            List<Book> ret = Connection.Query<Book>(sql, transaction: this.Transaction).ToList();
            return ret;
        }
        public Book Get(int id)
        {
            string sql = @"select * from Book where id=@id";
            Book ret = Connection.Query<Book>(sql, new { id }, transaction: this.Transaction).SingleOrDefault();
            return ret;
        }
        public bool Insert(Book book)
        {
            string sql = @"Insert into Book (id, author, price) values (@id, @author, @price)";
            int ret = Connection.Execute(sql, book, transaction: this.Transaction);
            return ret > 0 ? true : false;
        }
        public bool Update(Book book)
        {
            string sql = @"Update Book Set author=@author, price=@price where id=@id";
            int ret = Connection.Execute(sql, book, transaction: this.Transaction);
            return ret > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            string sql = @"delete Book where id=@id";
            int ret = Connection.Execute(sql, new { id }, transaction: this.Transaction);
            return ret > 0 ? true : false;
        }
        public void Dispose()
        {
            return;
        }
    }
}