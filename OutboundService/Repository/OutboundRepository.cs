/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DepotDatabase;
    using Microsoft.EntityFrameworkCore;
    using OutboundService.Models;

    /// <summary>
    /// Outbound service repository
    /// </summary>
    public class OutboundRepository :IOutboundRepository
    {
        private readonly DepotContext appDbContext;

        public OutboundRepository(DepotContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        /// <summary>
        /// Method to create the customer order
        /// </summary>
        /// <param name="customerOrderData">input parameters</param>
        /// <returns>data</returns>
        public async Task<CustomerOrder> CreateCustomerOrder(CustomerOrderData customerOrderData)
        {
            var discount = appDbContext.Discount.Where(c => c.ProductId == customerOrderData.ProductId && c.IsActive && c.StartDate < DateTime.Now && DateTime.Now < c.EndDate)
                                .Select(c => c.Id).FirstOrDefault();

            CustomerOrder customerOrder = new CustomerOrder()
            {
                ProductId = customerOrderData.ProductId,
                CreatedDate = DateTime.Now,
                UserId = customerOrderData.UserId,
                DiscountId = discount,
                Status = "Accepted",
                Quantity = customerOrderData.Quantity
            };
            var result = await appDbContext.CustomerOrder.AddAsync(customerOrder);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        /// <summary>
        /// Method to update order status
        /// </summary>
        /// <param name="orderStatus">input parameters</param>
        /// <returns>data</returns>
        public async Task<bool?> UpdateOrderStatus(OrderStatus orderStatus)
        {
            var order = await appDbContext.CustomerOrder.FirstOrDefaultAsync(c => c.Id == orderStatus.OrderId);
            if(order == null) { return null; }
            order.Status = orderStatus.Status;
            //appDbContext.CustomerOrder.Update(order);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method to update order quantity
        /// </summary>
        /// <param name="orderQuantity">input parameters</param>
        /// <returns>data</returns>
        public async Task<bool?> UpdateOrderQuantity(OrderQuantity orderQuantity)
        {
            var order = await appDbContext.CustomerOrder.FirstOrDefaultAsync(c => c.Id == orderQuantity.OrderId);
            if (order == null) { return null; }
            order.Quantity = orderQuantity.Quantity;
            //appDbContext.CustomerOrder.UpdateAsync(order);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method to update the order discount
        /// </summary>
        /// <param name="orderDiscount">input parameters</param>
        /// <returns>data</returns>
        public async Task<bool?> UpdateOrderDiscount(OrderDiscount orderDiscount)
        {
            var order = await appDbContext.CustomerOrder.FirstOrDefaultAsync(c => c.Id == orderDiscount.OrderId);
            if (order == null) { return null; }
            order.DiscountId = orderDiscount.DiscountId;
            appDbContext.CustomerOrder.Update(order);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method to create the customer order shipment
        /// </summary>
        /// <param name="shipmentData">input parameters</param>
        /// <returns>data</returns>
        public async Task<OrderShipment> CreateOrderShipment(OrderShipmentData shipmentData)
        {
            OrderShipment orderShipment = new OrderShipment()
            {
                CustomerOrderId = shipmentData.CustomerOrderId,
                TruckDetailId = shipmentData.TruckId,
                DriverDetailId = shipmentData.DriverId,
                Status = "In Progress",
            };
            var result = await appDbContext.OrderShipment.AddAsync(orderShipment);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        /// <summary>
        /// Method to update the shipment schedule
        /// </summary>
        /// <param name="shipmentData">input parameters</param>
        /// <returns>data</returns>
        public async Task<bool?> UpdateShipmentDate(ShipmentData shipmentData)
        {
            var shipment = await appDbContext.OrderShipment.FirstOrDefaultAsync(c => c.Id == shipmentData.ShipmentId);
            if (shipment == null) { return null; }
            shipment.Schedule = shipmentData.ShipmentDate;
            //appDbContext.OrderShipment.Update(shipment);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method to update the shipment status
        /// </summary>
        /// <param name="shipmentStatus">input parameters</param>
        /// <returns>data</returns>
        public async Task<bool?> UpdateShipmentStatus(ShipmentStatus shipmentStatus)
        {
            var shipment = await appDbContext.OrderShipment.FirstOrDefaultAsync(c => c.Id == shipmentStatus.ShipmentId);
            if (shipment == null) { return null; }
            shipment.Status = shipmentStatus.Status;
            //appDbContext.OrderShipment.Update(shipment);
            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
