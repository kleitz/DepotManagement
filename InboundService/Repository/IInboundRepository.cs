/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InboundService.Repository
{
    using System.Threading.Tasks;
    using DepotDatabase;
    using InboundService.Models;

    /// <summary>
    /// Inbound repository interface
    /// </summary>
    public interface IInboundRepository
    {
        Task<InboundOrder> CreateInboundOrder(InboundData inboundData);

        Task<Product> AssignPalletItem(int inboundOrderId);

        Task<Product> ReassignPalletItem(ReassignData reassignData);

        int GetItemQuantity(int productId);
    }
}
