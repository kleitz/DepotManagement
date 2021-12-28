/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Models
{
    /// <summary>
    /// Order status model for api
    /// </summary>
    public class OrderStatus
    {
        public int OrderId { get; set; }

        public string Status { get; set; }
    }
}
