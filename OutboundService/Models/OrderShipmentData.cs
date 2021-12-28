/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Models
{
    /// <summary>
    /// Order shipment data for api
    /// </summary>
    public class OrderShipmentData
    {
        public int CustomerOrderId { get; set; }

        public int TruckId { get; set; }

        public int DriverId { get; set; }
    }
}
