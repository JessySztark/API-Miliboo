using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_j_owning_own")]

    public class Owning
    {
        private int act_id;
        private int adr_id;

        [Key]
        [InverseProperty("act_id")]
        public int AccountID {
            get { return act_id; }
            set { act_id = value; }
        }

        [Key]
        [InverseProperty("adr_id")]
        public int AddressID {
            get { return adr_id; }
            set { adr_id = value; }
        }

        [ForeignKey("AccountID")]
        [InverseProperty("Addresses")]
        public virtual Account OwnerAccount { get; set; } = null!;

        [ForeignKey("AddressID")]
        [InverseProperty("Owners")]
        public virtual Address AddressOwned { get; set; } = null!;
    }
}
