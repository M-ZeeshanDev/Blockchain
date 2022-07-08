using System;
using System.ComponentModel.DataAnnotations;

namespace Blockchain.WebContracts
{
	public class CreateBlockRequest
	{
        [Required(ErrorMessage = "Book is required")]
        public BlockRequest BlockRequest { get; set; }
	}
}

