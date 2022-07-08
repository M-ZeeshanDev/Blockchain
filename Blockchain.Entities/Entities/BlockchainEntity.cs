using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blockchain.Entities
{
    [Table("Blockchain")]
    public class BlockchainEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey(nameof(TransactionStatus))]
        public int TransactionStatusId { get; set; }

        [ForeignKey(nameof(Block))]
        public long BlockId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual BlockEntity Block { get; set; }
        public virtual TransactionStatusEntity TransactionStatus { get; set; }
    }
}

