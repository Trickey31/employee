using aspnetcore.Application;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Infrastructure
{
    // sealed: không cho class khác kế thừa
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private DbConnection? _connection = null;
        
        private DbTransaction? _transaction = null;

        private readonly string _connectionString;
        #endregion


        #region Constructor
        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion


        public DbConnection Connection => _connection ??= new MySqlConnection(_connectionString);

        public DbTransaction? Transaction => _transaction;


        #region Methods
        public void BeginTransaction()
        {
            _connection ??= new MySqlConnection(_connectionString);

            if(_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
        }

        public async Task BeginTransactionAsync()
        {
            _connection ??= new MySqlConnection(_connectionString);

            if (_connection.State == ConnectionState.Open)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                await _connection.OpenAsync();
                _transaction = _connection.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            Dispose();
        }

        public async Task CommitTransactionAsync()
        {
            if(_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            await DisposeAsync();
        }

        public void Dispose()
        {
            if(_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            if(_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }

            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
            if ( _transaction != null )
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();
        }
        #endregion
    }
}
