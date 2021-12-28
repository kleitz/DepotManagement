/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Models
{
    /// <summary>
    /// Model representing the input for api updating the pallets lpn.
    /// </summary>
    public class PalletsLPNData
    {
        public int LpnId { get; set; }

        public int PalletId { get; set; }
    }
}
