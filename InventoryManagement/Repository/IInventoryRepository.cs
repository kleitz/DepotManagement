/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InventoryManagement.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DepotDatabase;
    using InventoryManagement.Models;

    /// <summary>
    /// Inventory management repository interface
    /// </summary>
    public interface IInventoryRepository
    {
        Task<List<PalletSearchResult>> GetPalletData(SearchFilter data);

        Task<ProductSearchResult> GetProductDetails(int productId);

        string GetProductLocation(int productId);

        Task<bool?> UpdateProductInformation(Product product);

        Task<string> GetOrderStatus(int orderId);

        Task<List<OrderSearchResult>> GetAllOrders(SearchFilter data);

        Task<List<Product>> SearchProduct(string productName);
    }
}
