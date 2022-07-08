using System;
namespace Blockchain.WebContracts
{
	public class TransactionResponse
	{
        public long Id { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Amount { get; set; }
        public long BlockId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}

