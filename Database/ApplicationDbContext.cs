using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FineTrack.Database
{
    public class ApplicationDbContext
    {
        public SQLiteAsyncConnection _dbConnection;

        //This is the Applcation Database  
        public readonly static string nameSpace = "FineTrack.Database.";

        public const string DatabaseFileName = "FineTrack.Database.db3";

        public static string databasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        // Constructor function which gets called itseld when the class is called
        // This checks if database is connected and if not connects to the databse
        // it also creates table named Transaction at the time of db connection every time
        // TODO: Need to add logic to see if a table already exists if not then only create a new table for all the tables
        public ApplicationDbContext()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new SQLiteAsyncConnection(databasePath, Flags);
                _dbConnection.CreateTableAsync<Transaction>();
                _dbConnection.CreateTableAsync<TransactionCategoryTable>();

            }
        }

        // This is a task for insertion to db table
        public async Task<int>CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return await _dbConnection.InsertAsync(entity);
        }

        // This is a task for updation in db table

        public async Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return await _dbConnection.UpdateAsync(entity);
        }

        // This is a task for deletion from db table

        public async Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return await _dbConnection.DeleteAsync(entity);
        }

        // This is a task for update and refresh to db table or something need to look at this 

        public async Task<int> AddOrUpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return await _dbConnection.InsertOrReplaceAsync(entity);
        }

        // This is a task for reading rows from a db table

        public List<T> GetTableRows<T>(string tableName) where T : class
        {
            object[] obj = new object[] { };
            TableMapping map = new TableMapping(Type.GetType(nameSpace + tableName));
            string query = "SELECT * FROM [" + tableName + "]";
            return _dbConnection.QueryAsync(map, query, obj).Result.Cast<T>().ToList();
        }

        // This is a task for reading a single row from a db table

        public object GetTableRow<T>(string tableName, string column, string value) where T : class
        {
            object[] obj = new object[] { };
            TableMapping map = new TableMapping(Type.GetType(nameSpace + tableName));
            string query = "SELECT * FROM " + tableName + " WHERE " +column+ "='" +value+"'";
            return _dbConnection.QueryAsync(map, query, obj).Result.FirstOrDefault();
        }
        public async Task<List<Transaction>> ReadAllAsync()
        {
            return await _dbConnection.Table<Transaction>().ToListAsync();
        }

        // Add a property to access the Transaction table
        public AsyncTableQuery<Transaction> Transactions => _dbConnection.Table<Transaction>();

        // Add a property to access the TransactionCategoryTable table
        public AsyncTableQuery<TransactionCategoryTable> TransactionCategories => _dbConnection.Table<TransactionCategoryTable>();


    }
}
