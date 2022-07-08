using System;
using Blockchain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Persistence
{
	public class BlockchainDbContext : DbContext
    {
		public BlockchainDbContext(DbContextOptions options)
            : base(options)
        {
		}

        /// <summary>
        /// Tables
        /// </summary>
        public DbSet<BlockchainEntity> Blockchains { get; set; }
        public DbSet<BlockEntity> Blocks { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        /// <summary>
        /// Lookups
        /// </summary>
        public DbSet<TransactionStatusEntity> TransactionStatuses { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BlockEntity>()
                .HasIndex(b => b.Index)
                .IsUnique();

            builder.Entity<BlockEntity>()
               .HasMany(b => b.Transactions)
               .WithOne(b => b.Block);
        }
    }
}

