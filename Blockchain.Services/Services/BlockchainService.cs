using System.Linq.Expressions;
using Blockchain.Entities;
using Blockchain.Enums;
using Blockchain.Persistence.Contracts;
using Blockchain.Services.Contracts;
using Blockchain.Services.Errors;
using Blockchain.WebContracts;

namespace Blockchain.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly IBlockchainRepository _blockchainRepository;

        public BlockchainService(IBlockchainRepository blockchainRepository)
		{
            _blockchainRepository = blockchainRepository;
        }

        public async Task<ServiceResult<BlockchainResponse>> CreateAsync(CreateBlockRequest request)
        {
            ServiceResult<BlockchainResponse> errorResult = new ServiceResult<BlockchainResponse>(System.Net.HttpStatusCode.BadRequest);
            if (request?.BlockRequest?.TransactionRequests == null || !request.BlockRequest.TransactionRequests.Any())
            {
                errorResult.AddError(nameof(ErrorMessages.R0001), ErrorMessages.R0001);
                return errorResult;
            }

            List<TransactionEntity> transactionEntities = new List<TransactionEntity>();
            foreach (TransactionRequest transactionRequest in request.BlockRequest.TransactionRequests)
            {
                TransactionEntity transactionEntity = new TransactionEntity
                {
                    FromAddress = transactionRequest.FromAddress,
                    ToAddress = transactionRequest.ToAddress,
                    Amount = transactionRequest.Amount,
                };
                transactionEntities.Add(transactionEntity);
            }

            BlockchainEntity blockchainEntity = new BlockchainEntity
            {
                Block = new BlockEntity
                {
                    Transactions = transactionEntities,
                },
            };

            blockchainEntity.TransactionStatusId = (int)TransactionStatusEnum.Panding;
            blockchainEntity = await _blockchainRepository.CreateAsync(blockchainEntity);
            blockchainEntity.TransactionStatusId = (int)TransactionStatusEnum.Success;
            blockchainEntity = await _blockchainRepository.UpdateAsync(blockchainEntity);
            blockchainEntity = await _blockchainRepository.GetByIdFullAsync(blockchainEntity.Id, isReadOnly: true);
            BlockchainResponse blockResponse = BlockchainMapper.MapToBlockchain(blockchainEntity);
            return new ServiceResult<BlockchainResponse>(blockResponse);
        }

        public async Task<ServiceResult<BlockchainResponse>> GetAsync(long id)
        {
            ServiceResult<BlockchainResponse> errorResult = new ServiceResult<BlockchainResponse>(System.Net.HttpStatusCode.BadRequest);

            BlockchainEntity blockchainEntity = await _blockchainRepository.GetByIdFullAsync(id, isReadOnly: true);
            if (blockchainEntity == null)
            {
                errorResult.AddError(nameof(ErrorMessages.B0001), ErrorMessages.B0001);
                return errorResult;
            }

            BlockchainResponse blockResponse = BlockchainMapper.MapToBlockchain(blockchainEntity);
            return new ServiceResult<BlockchainResponse>(blockResponse);
        }
    }
}

