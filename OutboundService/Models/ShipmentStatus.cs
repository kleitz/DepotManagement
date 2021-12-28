/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Models
{
    /// <summary>
    /// Shipment status model for api
    /// </summary>
    public class ShipmentStatus
    {
        public int ShipmentId { get; set; }

        public string Status { get; set; }
    }
}
