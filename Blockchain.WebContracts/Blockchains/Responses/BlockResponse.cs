using System;
using System.Collections.Generic;

namespace Blockchain.WebContracts
{
	public class BlockResponse
	{
        public long Id { get; set; }
        public long Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public long Nonce { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public List<TransactionResponse> Transactions { get; set; }
    }
}

