/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InventoryManagement.Models
{
    /// <summary>
    /// Search filter model for api
    /// </summary>
    public class SearchFilter
    {
        public int PageNumber { get; set; }

        public int RowsOfPage { get; set; }
    }
}
