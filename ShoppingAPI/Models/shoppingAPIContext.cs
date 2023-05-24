using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShoppingAPI.Models
{
    public partial class shoppingAPIContext : DbContext
    {
        public shoppingAPIContext()
        {
        }

        public shoppingAPIContext(DbContextOptions<shoppingAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<delivery> deliveries { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
        public virtual DbSet<userAccount> userAccounts { get; set; }
        public virtual DbSet<userType> userTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.paymentID).ValueGeneratedNever();

                entity.Property(e => e.date).HasColumnType("date");

                entity.Property(e => e.quantity)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.product)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.productID)
                    .HasConstraintName("FK__Payment__product__5812160E");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.userID)
                    .HasConstraintName("FK_Payment_userAccounts");
            });

            modelBuilder.Entity<cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.cartID).ValueGeneratedNever();

                entity.HasOne(d => d.product)
                    .WithMany(p => p.carts)
                    .HasForeignKey(d => d.productID)
                    .HasConstraintName("FK_cart_products");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.carts)
                    .HasForeignKey(d => d.userID)
                    .HasConstraintName("FK_cart_userAccounts");
            });

            modelBuilder.Entity<category>(entity =>
            {
                entity.Property(e => e.categoryID).ValueGeneratedNever();

                entity.Property(e => e.categoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.description).HasColumnType("text");
            });

            modelBuilder.Entity<delivery>(entity =>
            {
                entity.Property(e => e.deliveryID).ValueGeneratedNever();

                entity.Property(e => e.date).HasColumnType("date");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.deliveries)
                    .HasForeignKey(d => d.userID)
                    .HasConstraintName("FK_deliveries_userAccounts");
            });

            modelBuilder.Entity<order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.orderID).ValueGeneratedNever();

                entity.Property(e => e.date).HasColumnType("date");

                entity.HasOne(d => d.cart)
                    .WithMany(p => p.orders)
                    .HasForeignKey(d => d.cartID)
                    .HasConstraintName("FK_order_cart");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.orders)
                    .HasForeignKey(d => d.userID)
                    .HasConstraintName("FK_order_userAccounts");
            });

            modelBuilder.Entity<product>(entity =>
            {
                entity.Property(e => e.productID).ValueGeneratedNever();

                entity.Property(e => e.description).HasColumnType("text");

                entity.Property(e => e.name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.price)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.category)
                    .WithMany(p => p.products)
                    .HasForeignKey(d => d.categoryID)
                    .HasConstraintName("FK__products__catego__59063A47");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.products)
                    .HasForeignKey(d => d.userID)
                    .HasConstraintName("FK_products_userAccounts");
            });

            modelBuilder.Entity<transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.transactionID).ValueGeneratedNever();

                entity.Property(e => e.date).HasColumnType("date");

                entity.Property(e => e.description).HasColumnType("text");

                entity.Property(e => e.transactionType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.user)
                    .WithMany(p => p.transactions)
                    .HasForeignKey(d => d.userID)
                    .HasConstraintName("FK_transaction_userAccounts");
            });

            modelBuilder.Entity<userAccount>(entity =>
            {
                entity.HasKey(e => e.userID);

                entity.Property(e => e.userID).ValueGeneratedNever();

                entity.Property(e => e.address).HasColumnType("text");

                entity.Property(e => e.contactNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.gender)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.type)
                    .WithMany(p => p.userAccounts)
                    .HasForeignKey(d => d.typeID)
                    .HasConstraintName("FK_userAccounts_userTypes");
            });

            modelBuilder.Entity<userType>(entity =>
            {
                entity.HasKey(e => e.typeID);

                entity.Property(e => e.typeID).ValueGeneratedNever();

                entity.Property(e => e.description).HasColumnType("text");

                entity.Property(e => e.typeName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}