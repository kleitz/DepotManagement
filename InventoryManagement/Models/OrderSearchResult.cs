/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InventoryManagement.Models
{
    using System;

    /// <summary>
    /// Order search result for the api.
    /// </summary>
    public class OrderSearchResult
    {
        public DateTime CreatedDate { get; set; }

        public string ProductName { get; set; }

        public string OrderStatus { get; set; }

        public string ShipmentStatus { get; set; }

        public DateTime ShipmentDate { get; set; }
    }
}
