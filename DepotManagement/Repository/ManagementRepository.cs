/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Repository
{
    using System;
    using DepotManagement.Models;
    using DepotDatabase;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Management Repository to fetch/save the data for the api
    /// </summary>
    public class ManagementRepository :IManagementRepository
    {
        private readonly DepotContext appDbContext;

        public ManagementRepository(DepotContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<ProductType> AddProductType(ProductTypeData productTypeData)
        {
            ProductType productType = new ProductType()
            {
                Type = productTypeData .Type
            };
            var result = await appDbContext.ProductType.AddAsync(productType);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PalletQuantity> AddPalletQuantity(PalletQuantityData palletQuantityData)
        {
            PalletQuantity palletQuantity = new PalletQuantity()
            {
                MaxQuantity = palletQuantityData.MaxQuantity,
                ProductTypeId = palletQuantityData.ProductTypeId
            };
            var result = await appDbContext.PalletQuantity.AddAsync(palletQuantity);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<LPN> AddNodes(NodesData nodesData)
        {
            Nodes nodes = new Nodes()
            {
                Name = nodesData.Name,
                Location = nodesData.Location
            };
            var createdNode = await appDbContext.Nodes.AddAsync(nodes);
            LPN lPN = new LPN()
            {
                AddedDate = DateTime.Now,
                NodeId = createdNode.Entity.Id
            };
            var result = await appDbContext.LPN.AddAsync(lPN);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Pallet> AddPalletItemType(PalletsData palletsData)
        {
            //Product product = new Product()
            //{
            //    Name = palletsData.ProductData.Name,
            //    AddedDate = DateTime.Now,
            //};
            //var createdProduct = await appDbContext.Product.AddAsync(product);
            Pallet pallet = new Pallet()
            {
                LPNId = palletsData.LPNId ?? 0,
                //ProductTypeId = palletsData.ProductTypeId,
                PalletQuantityId = palletsData.PalletsQuantityId,
                //ProductId = createdProduct.Entity.Id,
                Quantity = palletsData.Quantity,
                Priority = "Low",
                
            };
            var result = await appDbContext.Pallet.AddAsync(pallet);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<LPN> GetLpn(int lpnId)
        {
            return await appDbContext.LPN
                .FirstOrDefaultAsync(e => e.Id == lpnId);
        }

        public async Task<bool?> UpdatePallet(PalletsLPNData palletsLPNData)
        {
            var result = await appDbContext.Pallet
               .FirstOrDefaultAsync(e => e.Id == palletsLPNData.PalletId);

            if(result != null)
            {
                result.LPNId = palletsLPNData.LpnId;
                await appDbContext.SaveChangesAsync();

                return true;
            }

            return null;
        }
    }
}
