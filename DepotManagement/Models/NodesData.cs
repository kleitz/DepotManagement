/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Models
{
    using System;

    /// <summary>
    /// NodesData Class represents the api input for Node creation
    /// </summary>
    public class NodesData
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
