/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Models
{
    using System;

    /// <summary>
    /// Shipment data model for api
    /// </summary>
    public class ShipmentData
    {
        public int ShipmentId { get; set; }

        public DateTime ShipmentDate { get; set; }
    }
}
