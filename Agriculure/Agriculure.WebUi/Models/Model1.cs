namespace Agriculure.WebUi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>()
                .HasMany(e => e.Contracts)
                .WithRequired(e => e.Offer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Offers)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Contracts)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.sellerID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Contracts1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.buyerID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Offers)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.offerowner);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
