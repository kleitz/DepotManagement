/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Repository
{
    using System.Threading.Tasks;
    using DepotDatabase;
    using OutboundService.Models;

    /// <summary>
    /// Outbound service repository interface
    /// </summary>
    public interface IOutboundRepository
    {
        /// <summary>
        /// Method to create the customer order
        /// </summary>
        /// <param name="customerOrderData">input parameters</param>
        /// <returns>data</returns>
        Task<CustomerOrder> CreateCustomerOrder(CustomerOrderData customerOrderData);

        /// <summary>
        /// Method to update order status
        /// </summary>
        /// <param name="orderStatus">input parameters</param>
        /// <returns>data</returns>
        Task<bool?> UpdateOrderStatus(OrderStatus orderStatus);

        /// <summary>
        /// Method to update order quantity
        /// </summary>
        /// <param name="orderQuantity">input parameters</param>
        /// <returns>data</returns>
        Task<bool?> UpdateOrderQuantity(OrderQuantity orderQuantity);

        /// <summary>
        /// Method to update the order discount
        /// </summary>
        /// <param name="orderDiscount">input parameters</param>
        /// <returns>data</returns>
        Task<bool?> UpdateOrderDiscount(OrderDiscount orderDiscount);

        /// <summary>
        /// Method to create the customer order shipment
        /// </summary>
        /// <param name="shipmentData">input parameters</param>
        /// <returns>data</returns>
        Task<OrderShipment> CreateOrderShipment(OrderShipmentData shipmentData);

        /// <summary>
        /// Method to update the shipment schedule
        /// </summary>
        /// <param name="shipmentData">input parameters</param>
        /// <returns>data</returns>
        Task<bool?> UpdateShipmentDate(ShipmentData shipmentData);

        /// <summary>
        /// Method to update the shipment status
        /// </summary>
        /// <param name="shipmentStatus">input parameters</param>
        /// <returns>data</returns>
        Task<bool?> UpdateShipmentStatus(ShipmentStatus shipmentStatus);
    }
}
