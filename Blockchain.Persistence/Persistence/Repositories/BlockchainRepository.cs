using System;
using System.Linq.Expressions;
using Blockchain.Entities;
using Blockchain.Enums;
using Blockchain.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blockchain.Persistence.Repositories
{
	public class BlockchainRepository : IBlockchainRepository
    {
        protected readonly BlockchainDbContext _dbContext;
        private DateTime utcNow = DateTime.UtcNow;

        public BlockchainRepository(BlockchainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlockchainEntity> CreateAsync(BlockchainEntity blockchainEntity)
        {
            BlockEntity latestBlock = await GetLastAsync(isReadOnly: false);
            if (latestBlock == null)
            {
                blockchainEntity.Block.Index = 0;
                blockchainEntity.Block.PreviousHash = string.Empty;
            }
            else
            {
                blockchainEntity.Block.Index = latestBlock.Index + 1;
                blockchainEntity.Block.PreviousHash = latestBlock.Hash;
            }
            
            blockchainEntity.Block.TimeStamp = utcNow;
            blockchainEntity.Block.Mine(2);
            blockchainEntity.Block.UpdatedOn = utcNow;
            blockchainEntity.Block.CreatedOn = utcNow;
            
            blockchainEntity.TransactionStatusId = (int)TransactionStatusEnum.Success;
            blockchainEntity.UpdatedOn = utcNow;
            blockchainEntity.CreatedOn = utcNow;

            foreach (TransactionEntity transactionEntity in blockchainEntity.Block.Transactions)
            {
                transactionEntity.UpdatedOn = utcNow;
                transactionEntity.CreatedOn = utcNow;
            }

            EntityEntry<BlockchainEntity> entry = await _dbContext.Blockchains.AddAsync(blockchainEntity);
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<BlockchainEntity> GetByIdAsync(long id, bool isReadOnly, Expression<Func<BlockchainEntity, BlockchainEntity>> fields = null)
        {
            IQueryable<BlockchainEntity> query = _dbContext.Blockchains;
            if (isReadOnly)
            {
                query = query.AsNoTracking();
            }

            if (fields != null)
            {
                query = query.Select(fields);
            }

            return await query
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlockchainEntity> GetByIdFullAsync(long id, bool isReadOnly)
        {
            IQueryable<BlockchainEntity> query = _dbContext.Blockchains;
            if (isReadOnly)
            {
                query = query.AsNoTracking();
            }

            return await query
                .Include(x => x.Block)
                    .ThenInclude(x => x.Transactions)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlockEntity> GetLastAsync(bool isReadOnly)
        {
            IQueryable<BlockEntity> query = _dbContext.Blocks;
            if (isReadOnly)
            {
                query = query.AsNoTracking();
            }
            return await query.OrderBy(b => b.Id).LastOrDefaultAsync();
        }

        public async Task<BlockchainEntity> UpdateAsync(BlockchainEntity blockchainEntity)
        {
            _dbContext.Blockchains.Update(blockchainEntity);
            await _dbContext.SaveChangesAsync();
            return blockchainEntity;
        }
    }
}

