/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InventoryManagement.Models
{
    /// <summary>
    /// Pallet search result model for api
    /// </summary>
    public class PalletSearchResult
    {
        public int MaxQuantity { get; set; }

        public int Quantity { get; set; }

        public string ProductType { get; set; }

        public string NodeLocation { get; set; }

        public string Priority { get; set; }
    }
}
