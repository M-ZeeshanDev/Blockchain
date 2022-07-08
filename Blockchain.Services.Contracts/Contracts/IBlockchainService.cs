using System;
using Blockchain.WebContracts;

namespace Blockchain.Services.Contracts
{
	public interface IBlockchainService
	{
        Task<ServiceResult<BlockchainResponse>> GetAsync(long id);
        Task<ServiceResult<BlockchainResponse>> CreateAsync(CreateBlockRequest request);
    }
}

