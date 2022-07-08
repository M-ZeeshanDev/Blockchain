using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blockchain.Entities
{
    [Table("Transactions")]
    public class TransactionEntity
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Amount { get; set; }

        [ForeignKey(nameof(Block))]
        public long BlockId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public BlockEntity Block { get; set; }
    }
}

