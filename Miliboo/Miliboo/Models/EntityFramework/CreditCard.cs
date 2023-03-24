using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Miliboo.Models.EntityFramework;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_creditcard_crc")]
    public class CreditCard
    {
        private int cardID;
        private int accountID;
        private String? name;
        private String? firstName;
        private DateTime expirationDate;
        private String cardNumber;
        private String? cryptogram;

        [Key]
        [Column("crc_cardid")]
        public int CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }

        [Column("crc_accountid")]
        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        [Column("crc_name", TypeName = "varchar(50)")]
        public String? Name
        {
            get { return name; }
            set { name = value; }
        }

        [Column("crc_firstname", TypeName = "varchar(50)")]

        public String? FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [Column("crc_expirationdate")]
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        [Column("crc_cardnumber", TypeName = "varchar(50)")]
        [Required]
        public String CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }

        [Column("crc_cryptogram", TypeName = "varchar(3)")]
        public String? Cryptogram
        {
            get { return cryptogram; }
            set { cryptogram = value; }
        }



        [ForeignKey("AccountID")]
        [InverseProperty("CreditCardAccount")]
        public virtual Account AccountCreditCard { get; set; } = null!;


        [InverseProperty("CreditCardOrder")]
        public virtual ICollection<Order> OrderCreditCard { get; set; } = new List<Order>();
    }
}
