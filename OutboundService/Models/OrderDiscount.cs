/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Models
{
    /// <summary>
    /// Class represents Order discount
    /// </summary>
    public class OrderDiscount
    {
        public int OrderId { get; set; }

        public int? DiscountId { get; set; }
    }
}
