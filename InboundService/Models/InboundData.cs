/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InboundService.Models
{
    using System;

    /// <summary>
    /// Inbound data model for the api
    /// </summary>
    public class InboundData
    {
        public DateTime CreatedDate { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductName { get; set; }

        public string Supplier { get; set; }
    }
}
