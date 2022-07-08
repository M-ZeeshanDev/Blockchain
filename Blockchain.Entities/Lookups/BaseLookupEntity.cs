using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blockchain.Entities
{
	public class BaseLookupEntity<T> where T : struct, Enum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(120)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        protected BaseLookupEntity()
        {
        }

        protected BaseLookupEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public T? Enum
        {
            get
            {
                T result;
                string name = Name;
                if (Parse(out result, name))
                {
                    return result;
                }
                string nameWithoutSpaces = Name.Replace(" ", string.Empty);
                if (Parse(out result, nameWithoutSpaces))
                {
                    return result;
                }
                if (Parse(out result, "unknown"))
                {
                    return result;
                }
                return default;
            }
        }

        private static bool Parse(out T result, string name)
        {
            return System.Enum.TryParse(name, true, out result);
        }
    }
}
