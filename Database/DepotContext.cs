/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotDatabase
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the context of DepotManagement.
    /// </summary>
    public class DepotContext : DbContext
    {      
        public DepotContext(DbContextOptions<DepotContext> options): base(options){ }

        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<PalletQuantity> PalletQuantity { get; set; }

        public DbSet<Pallet> Pallet { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<LPN> LPN { get; set; }

        public DbSet<Nodes> Nodes { get; set; }

        public DbSet<InboundOrder> InboundOrder { get; set; }

        public DbSet<CustomerOrder> CustomerOrder { get; set; }

        public DbSet<Discount> Discount { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<TruckDetail> TruckDetails { get; set; }

        public DbSet<DriverDetail> DriverDetails { get; set; }

        public DbSet<OrderShipment> OrderShipment { get; set; }      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var key in cascadeFKs)
                key.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

    }
}
