using MyPurchases.Models;
using SQLite;

namespace MyPurchases.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _connection;
        public SQLiteDatabaseHelper(string path) 
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Product>().Wait();
        }

        public Task<int> Insert(Product product) 
        {
            return _connection.InsertAsync(product);
        }

        public Task<List<Product>> Update(Product product) 
        {
            string sql = "UPDATE Product SET Description=?, Amount=?, Price=? WHERE Id=?";

            return _connection.QueryAsync<Product>(sql,
                product.Description, product.Amount, product.Price, product.Id);
        }

        public Task<int> Delete(int id) 
        {
            return _connection.Table<Product>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Product>> GetAll() 
        {
            return _connection.Table<Product>().ToListAsync();
        }

        public Task<List<Product>> Search(String query) {
            string sql = "SELECT * FROM Product WHERE Description LIKE '%" + query + "%'";

            return _connection.QueryAsync<Product>(sql);
        }
    }
}
