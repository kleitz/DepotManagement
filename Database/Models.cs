/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotDatabase
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// BaseModel that represents PrimaryKey for all the models.
    /// </summary>
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }

    /// <summary>
    /// Represent ProductType model.
    /// </summary>
    public class ProductType : BaseModel
    {
        [Required]
        public string Type { get; set; }
    }

    /// <summary>
    /// Represent PalletQuantity model.
    /// </summary>
    public class PalletQuantity : BaseModel
    {
        [Required]
        public int MaxQuantity { get; set; }

        [Required]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }

    /// <summary>
    /// Represent Pallet model.
    /// </summary>
    public class Pallet : BaseModel
    {

        [Required]
        public int PalletQuantityId { get; set; }
        public virtual PalletQuantity PalletQuantity { get; set; }

        public int Quantity { get; set; }

        public int? LPNId { get; set; }
        public virtual LPN LPN { get; set; }

        public string Priority { get; set; }
    }

    /// <summary>
    /// Represent Product model.
    /// </summary>
    public class Product : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        [Required]
        public int PalletId { get; set; }
        public virtual Pallet Pallet { get; set; }
    }

    /// <summary>
    /// Represent LPN model.
    /// </summary>
    public class LPN : BaseModel
    {
        [Required]
        public int NodeId { get; set; }
        public virtual Nodes Nodes { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }
    }

    /// <summary>
    /// Represent Nodes model.
    /// </summary>
    public class Nodes : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }
    }

    /// <summary>
    /// Represent Inbound model.
    /// </summary>
    public class InboundOrder : BaseModel
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string Status { get; set; }

        [Required]
        public string Supplier { get; set; }
    }

    /// <summary>
    /// Represent Discount model.
    /// </summary>
    public class Discount : BaseModel
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }

    /// <summary>
    /// Represent User model.
    /// </summary>
    public class User: BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }

    /// <summary>
    /// Represent CustomerOrder model.
    /// </summary>
    public class CustomerOrder : BaseModel
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int? DiscountId { get; set; }
        public virtual Discount Discount { get; set; }

        public string Status { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Represent TruckDetail model.
    /// </summary>
    public class TruckDetail : BaseModel
    {
        [Required]
        public string Number { get; set; }
    }

    /// <summary>
    /// Represent DriverDetail model.
    /// </summary>
    public class DriverDetail : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents OrderShipment model.
    /// </summary>
    public class OrderShipment : BaseModel
    {
        [Required]
        public int CustomerOrderId { get; set; }
        public virtual CustomerOrder CustomerOrder { get; set; }

        [Required]
        public int TruckDetailId { get; set; }
        public virtual TruckDetail TruckDetail { get; set; }

        [Required]
        public int DriverDetailId { get; set; }
        public virtual DriverDetail DriverDetail { get; set; }

        public string Status { get; set; }

        public DateTime Schedule { get; set; }
    }
}
