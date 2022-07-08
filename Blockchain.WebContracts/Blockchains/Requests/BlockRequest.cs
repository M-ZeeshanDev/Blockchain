using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blockchain.WebContracts
{
	public class BlockRequest
	{
        public BlockRequest()
        {
            TransactionRequests = new List<TransactionRequest>();
        }

        [Required(ErrorMessage = "Transaction is required")]
		public List<TransactionRequest> TransactionRequests { get; set; }
	}
}

