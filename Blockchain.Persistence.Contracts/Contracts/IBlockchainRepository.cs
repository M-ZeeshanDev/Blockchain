using System;
using System.Linq.Expressions;
using Blockchain.Entities;

namespace Blockchain.Persistence.Contracts
{
	public interface IBlockchainRepository
	{
        Task<BlockchainEntity> GetByIdFullAsync(long id, bool isReadOnly);
        Task<BlockchainEntity> GetByIdAsync(long id, bool isReadOnly, Expression<Func<BlockchainEntity, BlockchainEntity>> fields = null);
        Task<BlockchainEntity> CreateAsync(BlockchainEntity blockchainEntity);
        Task<BlockEntity> GetLastAsync(bool isReadOnly);
        Task<BlockchainEntity> UpdateAsync(BlockchainEntity blockchainEntity);
    }
}

