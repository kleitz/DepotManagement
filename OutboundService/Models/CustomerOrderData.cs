/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Models
{
    using System;

    /// <summary>
    /// Class representing Customer Order.
    /// </summary>
    public class CustomerOrderData
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }
    }
}
