using System;
using Blockchain.Entities;
using Blockchain.WebContracts;

namespace Blockchain.Services
{
	public static class BlockchainMapper
	{
		public static BlockchainResponse MapToBlockchain(BlockchainEntity blockchainEntity)
        {
            BlockResponse blockResponse = null;
            if (blockchainEntity.Block != null)
            {
                blockResponse = MapToBlock(blockchainEntity.Block);
            }

            return new BlockchainResponse
            {
                Id = blockchainEntity.Id,
                TransactionStatusId = blockchainEntity.TransactionStatusId,
                BlockId = blockchainEntity.BlockId,
                UpdatedOn = blockchainEntity.UpdatedOn,
                CreatedOn = blockchainEntity.CreatedOn,
                Block = blockResponse,
            };
        }

        public static BlockResponse MapToBlock(BlockEntity blockEntity)
        {
            List<TransactionResponse> transactionResponses = new List<TransactionResponse>();
            if (blockEntity?.Transactions != null)
            {
                transactionResponses = MapToTransactionList(blockEntity.Transactions);
            }

            return new BlockResponse
            {
                Id = blockEntity.Id,
                Index = blockEntity.Index,
                PreviousHash = blockEntity.PreviousHash,
                Hash = blockEntity.Hash,
                TimeStamp = blockEntity.TimeStamp,
                Nonce = blockEntity.Nonce,
                Transactions = transactionResponses,
                UpdatedOn = blockEntity.UpdatedOn,
                CreatedOn = blockEntity.CreatedOn,
            };
        }

        public static List<TransactionResponse> MapToTransactionList(ICollection<TransactionEntity> transactionEntities)
        {
            List<TransactionResponse> transactionResponses = new List<TransactionResponse>();
            foreach (TransactionEntity transactionEntity in transactionEntities)
            {
                transactionResponses.Add(MapToTransaction(transactionEntity));
            }
            return transactionResponses;
        }

        public static TransactionResponse MapToTransaction(TransactionEntity transactionEntity)
        {
            return new TransactionResponse
            {
                Id = transactionEntity.Id,
                FromAddress = transactionEntity.FromAddress,
                ToAddress = transactionEntity.ToAddress,
                Amount = transactionEntity.Amount,
                BlockId = transactionEntity.BlockId,
                UpdatedOn = transactionEntity.UpdatedOn,
                CreatedOn = transactionEntity.CreatedOn,
            };
        }
    }
}

