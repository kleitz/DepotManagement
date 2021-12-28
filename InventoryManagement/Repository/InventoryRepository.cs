/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InventoryManagement.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using DepotDatabase;
    using InventoryManagement.Models;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Inventory management repository
    /// </summary>
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DepotContext appDbContext;

        public InventoryRepository(DepotContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<PalletSearchResult>> GetPalletData(SearchFilter data)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@PageNumber",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Value = data.PageNumber
                        },
                        new SqlParameter() {
                            ParameterName = "@RowsOfPage",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Value = data.RowsOfPage
                        }};
            using (var connection = appDbContext.Database.GetDbConnection())
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "[dbo].[GetPalletDetails]";
                command.Parameters.AddRange(param);
                command.Connection = connection;
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                List<PalletSearchResult> searchResult = new List<PalletSearchResult>();
                searchResult = DataReaderMapToList<PalletSearchResult>(reader);

                return searchResult;
            }

        }

        public async Task<ProductSearchResult> GetProductDetails(int productId)
        {
            var result = await appDbContext.Product.FirstOrDefaultAsync(p => p.Id == productId);
            ProductSearchResult productSearchResult = new ProductSearchResult()
            {
                Name = result.Name,
                AddedDate = result.AddedDate,
            };
            return productSearchResult;
        }

        public string GetProductLocation(int productId)
        {
            return appDbContext.Product.Where(p => p.Id == productId).Select(c => c.Pallet.LPN.Nodes.Location).ToString();
        }

        public async Task<bool?> UpdateProductInformation(Product product)
        {
            var productRecord = await appDbContext.Product.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (productRecord == null) { return null; }
            productRecord = product;
            await appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetOrderStatus(int orderId)
        {
            var result = await appDbContext.OrderShipment.FirstOrDefaultAsync(c => c.CustomerOrderId == orderId);
            return result.Status;
        }

        public async Task<List<OrderSearchResult>> GetAllOrders(SearchFilter data)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@PageNumber",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Value = data.PageNumber
                        },
                        new SqlParameter() {
                            ParameterName = "@RowsOfPage",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Value = data.RowsOfPage
                        }};
            using (var connection = appDbContext.Database.GetDbConnection())
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "[dbo].[GetAllOrders]";
                command.Parameters.AddRange(param);
                command.Connection = connection;
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                List<OrderSearchResult> searchResult = new List<OrderSearchResult>();
                searchResult = DataReaderMapToList<OrderSearchResult>(reader);

                return searchResult;
            }
        }

        public async Task<List<Product>> SearchProduct(string productName)
        {
            var result = await appDbContext.Product.Where(c => c.Name.Contains(productName)).ToListAsync();
            return result;
        }

        /// <summary>
        /// Method to convert data reader to list
        /// </summary>
        /// <typeparam name="T">represents object type</typeparam>
        /// <param name="dataReader">Represents data reader object</param>
        /// <returns>returns list</returns>
        private static List<T> DataReaderMapToList<T>(IDataReader dataReader)
        {
            List<T> list = new List<T>();
            while (dataReader.Read())
            {
                T obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dataReader[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dataReader[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
