using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Miliboo.Models.EntityFramework {
    [Table("T_E_ACCOUNT_ACT")]
    public class Account {
        private int act_id;
        private String? act_password;
        private String? act_firstname;
        private String? act_lastname;
        private String? act_mail;
        private String? act_phonenumber;
        private bool act_oath;

        public Account() {
            Addresses = new HashSet<Owning>();
        }

        [Key]
        [Column("act_id")]
        [Required]
        public int AccountID {
            get { return act_id; }
            set { act_id = value; }
        }

        [Column("act_password", TypeName="varchar")]
        [MinLength(7)]
        [MaxLength(30)]
        [Required]
        public String? Password {
            get { return act_password; }
            set { act_password = value; }
        }

        [Column("act_firstname", TypeName = "varchar")]
        [MaxLength(20)]
        [Required]
        public String? FirstName {
            get { return act_firstname; }
            set { act_firstname = value; }
        }

        [Column("act_lastname", TypeName = "varchar")]
        [MaxLength(20)]
        [Required]
        public String? LastName {
            get { return act_lastname; }
            set { act_lastname = value; }
        }

        [Column("act_mail", TypeName = "varchar")]
        [MaxLength(50)]
        [Required]
        public String? Mail {
            get { return act_mail; }
            set { act_mail = value; }
        }

        [Column("act_phonenumber", TypeName = "char(10)")]
        public String? PhoneNumber {
            get { return act_phonenumber; }
            set { act_phonenumber = value; }
        }

        [Column("act_oath", TypeName = "bool")]
        public bool Oath {
            get { return act_oath; }
            set { act_oath = value; }
        }

        public virtual ICollection<Owning> Addresses { get; set; }



        public virtual ICollection<Order> OrderAccount { get; set; }

    }
}
