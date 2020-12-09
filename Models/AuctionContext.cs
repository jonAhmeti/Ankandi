using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Auction.Models
{
    public partial class AuctionContext : DbContext
    {
        public AuctionContext()
        {
        }

        public AuctionContext(DbContextOptions<AuctionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuctionData> AuctionData { get; set; }
        public virtual DbSet<BidHistory> BidHistory { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WithdrawHistory> WithdrawHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=Auction");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionData>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BidHistory>(entity =>
            {
                entity.Property(e => e.BidAmount).HasColumnType("money");

                entity.Property(e => e.BidDate).HasColumnType("datetime");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.BidHistory)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__BidHistor__Aucti__7908F585");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.BidHistory)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK__BidHistor__Event__7814D14C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BidHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BidHistor__UserI__0D0FEE32");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.CurrentPrice).HasColumnType("money");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Lud)
                    .HasColumnName("LUD")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lun)
                    .HasColumnName("LUN")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MinPriceIncrementAmount).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__Event__AuctionId__74444068");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__Event__ItemId__73501C2F");

                entity.HasOne(d => d.TopBidderNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.TopBidder)
                    .HasConstraintName("FK__Event__TopBidder__7073AF84");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Details)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.InD)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Lud)
                    .HasColumnName("LUD")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lun)
                    .HasColumnName("LUN")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MeasurementUnits)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sold).HasDefaultValueSql("((0))");

                entity.Property(e => e.SoldDate).HasColumnType("datetime");

                entity.Property(e => e.SoldPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ__Roles__2CB664DC45134B71")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__536C85E49EFD3461")
                    .IsUnique();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.InD)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Lud)
                    .HasColumnName("LUD")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Lun).HasColumnName("LUN");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasDefaultValueSql("((2))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__RoleId__640DD89F");
            });

            modelBuilder.Entity<WithdrawHistory>(entity =>
            {
                entity.Property(e => e.WithdrawAmount).HasColumnType("money");

                entity.Property(e => e.WithdrawDate).HasColumnType("datetime");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.WithdrawHistory)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__WithdrawH__Aucti__7DCDAAA2");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.WithdrawHistory)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK__WithdrawH__Event__7CD98669");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WithdrawHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WithdrawH__UserI__0C1BC9F9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
