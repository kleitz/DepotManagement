/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Models
{
    using System;

    /// <summary>
    /// Model representing the input for api of product creation
    /// </summary>
    public class ProductData
    {
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
