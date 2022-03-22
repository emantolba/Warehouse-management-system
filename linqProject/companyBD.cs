using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace linqProject
{
    public partial class companyBD : DbContext
    {
        public companyBD()
            : base("name=companyBD")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<exchangePermition> exchangePermitions { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<item_exchamgePermition> item_exchamgePermition { get; set; }
        public virtual DbSet<item_supplyPermition> item_supplyPermition { get; set; }
        public virtual DbSet<Item_unit> Item_unit { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<supplyPermition> supplyPermitions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<transfare> transfares { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.phone)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.fax)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.mail)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.exchangePermitions)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.client_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<exchangePermition>()
                .HasMany(e => e.item_exchamgePermition)
                .WithRequired(e => e.exchangePermition)
                .HasForeignKey(e => e.EXP_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .HasMany(e => e.item_exchamgePermition)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.item_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.item_supplyPermition)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.item_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Item_unit)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.transfares)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Stores)
                .WithMany(e => e.Items)
                .Map(m => m.ToTable("store_items").MapLeftKey("Item_code").MapRightKey("store_id"));

            modelBuilder.Entity<Item_unit>()
                .Property(e => e.unit_name)
                .IsFixedLength();

            modelBuilder.Entity<Store>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Store>()
                .Property(e => e.address)
                .IsFixedLength();

            modelBuilder.Entity<Store>()
                .Property(e => e.manager)
                .IsFixedLength();

            modelBuilder.Entity<Store>()
                .HasMany(e => e.exchangePermitions)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.supplyPermitions)
                .WithRequired(e => e.Store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.transfares)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.from_store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.transfares1)
                .WithRequired(e => e.Store1)
                .HasForeignKey(e => e.to_store)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.phone)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.fax)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.mail)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Items)
                .WithOptional(e => e.Supplier)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.supplyPermitions)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.transfares)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<supplyPermition>()
                .HasMany(e => e.item_supplyPermition)
                .WithRequired(e => e.supplyPermition)
                .HasForeignKey(e => e.SP_id)
                .WillCascadeOnDelete(false);
        }
    }
}
