using System;
using System.ComponentModel.DataAnnotations.Schema;
using Blockchain.Enums;

namespace Blockchain.Entities
{
    [Table("TransactionStatus", Schema = "lookup")]
    public class TransactionStatusEntity : BaseLookupEntity<TransactionStatusEnum>
    {
        public TransactionStatusEntity() { }

        public TransactionStatusEntity(int id, string name) : base(id, name) { }
    }
}

