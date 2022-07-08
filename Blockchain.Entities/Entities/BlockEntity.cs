using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Entities
{
    [Table("Blocks")]
    public class BlockEntity
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public long Nonce { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<TransactionEntity> Transactions { get; set; }

        [InverseProperty("Block")]
        public virtual BlockchainEntity Blockchain { get; set; }


        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = CalculateHash();
            }
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Newtonsoft.Json.JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

    }
}

