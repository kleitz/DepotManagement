/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Models
{
    /// <summary>
    /// Model representing the input for api pallet creation
    /// </summary>
    public class PalletsData
    {      
        public int PalletsQuantityId { get; set; }

        public int? LPNId { get; set; }

        public int Quantity { get; set; }

    }
}
