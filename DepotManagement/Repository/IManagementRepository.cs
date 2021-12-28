/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Repository
{
    using System.Threading.Tasks;
    using DepotDatabase;
    using DepotManagement.Models;

    /// <summary>
    /// Management repository interface.
    /// </summary>
    public interface IManagementRepository
    {
        Task<ProductType> AddProductType(ProductTypeData productTypeData);

        Task<PalletQuantity> AddPalletQuantity(PalletQuantityData palletQuantityData);

        Task<LPN> AddNodes(NodesData nodesData);

        Task<Pallet> AddPalletItemType(PalletsData palletsData);

        Task<LPN> GetLpn(int Id);

        Task<bool?> UpdatePallet(PalletsLPNData palletsLPNData);
    }
}
