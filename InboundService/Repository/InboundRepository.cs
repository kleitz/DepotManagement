/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InboundService.Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DepotDatabase;
    using InboundService.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Inbound repository
    /// </summary>
    public class InboundRepository : IInboundRepository
    {
        private readonly DepotContext appDbContext;

        public InboundRepository(DepotContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<InboundOrder> CreateInboundOrder(InboundData inboundData)
        {
            var palletsQuantity = appDbContext.PalletQuantity.Where(c => c.ProductTypeId == inboundData.ProductTypeId).FirstOrDefault();
            var palletsList = appDbContext.Pallet.Where(c => c.PalletQuantityId == palletsQuantity.Id).Select(c => c.Quantity).ToList();
            string status = "reject";
            foreach(var quantity in palletsList)
            {
                if(quantity < palletsQuantity.MaxQuantity)
                {
                    status = "accept";
                    break;
                }
            }

            InboundOrder inboundOrder = new InboundOrder()
            {
                CreatedDate = DateTime.Now,
                ProductTypeId = inboundData.ProductTypeId,
                ProductName = inboundData.ProductName,
                Status = status
            };
            var result = await appDbContext.InboundOrder.AddAsync(inboundOrder);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> AssignPalletItem(int inboundOrderId)
        {
            var order = await appDbContext.InboundOrder.FirstOrDefaultAsync(c => c.Id == inboundOrderId);

            var palletsQuantity = appDbContext.PalletQuantity.Where(c => c.ProductTypeId == order.ProductTypeId).FirstOrDefault();
            var pallet = appDbContext.Pallet.Where(c => c.PalletQuantityId == palletsQuantity.Id && c.Quantity < palletsQuantity.MaxQuantity).FirstOrDefault();

            
            Product product = new Product()
            {
                AddedDate = DateTime.Now,
                Name = order.ProductName,
                PalletId = pallet.Id
            };
            var result = await appDbContext.Product.AddAsync(product);

            //Modify Pallets Quantity and Priority
            pallet.Quantity++;
            pallet.Priority = pallet.Quantity <= (palletsQuantity.MaxQuantity / 2) ? "Medium" : "High";
            appDbContext.Pallet.Update(pallet);

            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> ReassignPalletItem(ReassignData reassignData)
        {
            var product = appDbContext.Product.FirstOrDefault(c => c.Id == reassignData.ProductId);
            product.PalletId = reassignData.PalletId;
            appDbContext.Product.Update(product);
            await appDbContext.SaveChangesAsync();
            return product;
        }

        public int GetItemQuantity(int productId)
        {
            var quantity = appDbContext.Product.Where(c => c.Id == productId).Select(c=> c.Pallet.Quantity).FirstOrDefault();
            return quantity;

        }
    }
}
