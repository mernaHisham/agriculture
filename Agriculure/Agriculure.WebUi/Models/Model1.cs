namespace Agriculure.WebUi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<FarmSupplier> FarmSuppliers { get; set; }
        public virtual DbSet<Logistic> Logistics { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Retailer> Retailers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farmer>()
                .HasMany(e => e.Producers)
                .WithRequired(e => e.Farmer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Farmer>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Farmer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FarmSupplier>()
                .HasMany(e => e.Farmers)
                .WithRequired(e => e.FarmSupplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FarmSupplier>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.FarmSupplier)
                .HasForeignKey(e => e.FarmerSID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Logistic>()
                .HasMany(e => e.Retailers)
                .WithRequired(e => e.Logistic)
                .HasForeignKey(e => e.LogisticsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Logistic>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Logistic)
                .HasForeignKey(e => e.LogisticsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Logistics)
                .WithRequired(e => e.Producer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Retailers)
                .WithRequired(e => e.Producer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Producer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Retailer>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Retailer)
                .WillCascadeOnDelete(false);
        }
    }
}
