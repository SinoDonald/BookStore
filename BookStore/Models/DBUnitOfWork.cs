﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class DBUnitOfWork : IDisposable
    {
        //插入 Repository
        private BookDBRepository _bookRepository;
        public BookDBRepository Book
        {
            get { return _bookRepository ?? (_bookRepository = new BookDBRepository(this.Transaction)); }
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;
        public DBUnitOfWork() : this(ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString)
        {

        }
        public DBUnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }
        public IDbTransaction Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _connection.Open();
                    _transaction = _connection.BeginTransaction();
                }
                return _transaction;
            }
        }
        //---------------------------
        //重設 Repository
        private void ResetRepositories()
        {
            //_flowQueueRepository = null;
        }
        public virtual void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                this.ResetRepositories();
            }
        }
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
        ~DBUnitOfWork()
        {
            dispose(false);
        }
    }
}