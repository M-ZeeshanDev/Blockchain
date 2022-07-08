using System;
namespace Blockchain.WebContracts
{
	public class BlockchainResponse
	{
        public long Id { get; set; }
        public int TransactionStatusId { get; set; }
        public long BlockId { get; set; }
        public BlockResponse Block { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}

