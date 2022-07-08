using System;
using System.ComponentModel.DataAnnotations;

namespace Blockchain.WebContracts
{
	public class TransactionRequest
	{
        [Required(ErrorMessage = "FromAddress is required")]
        public string FromAddress { get; set; }

        [Required(ErrorMessage = "ToAddress is required")]
        public string ToAddress { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public int Amount { get; set; }
    }
}

