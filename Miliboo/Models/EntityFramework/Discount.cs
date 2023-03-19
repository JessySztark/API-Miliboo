using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_discount_dsc")]

    public class Discount
    {
        private int discountID;
        private String discountName;
        private bool isActive;
        private decimal discountValue;


        [Key]
        [Required]
        [Column("dsc_discountid")]
        public int DiscountID
        {
            get { return discountID; }
            set { discountID = value; }
        }

        [Column("dsc_discountname", TypeName ="varchar")]
        [Required]
        public String DiscountName
        {
            get { return discountName; }
            set { discountName = value; }
        }

        [Column("dsc_isactive", TypeName = "bool")]
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [Column("dsc_value", TypeName = "numeric")]
        public decimal DiscountValue
        {
            get { return discountValue; }
            set { discountValue = value; }
        }

        [InverseProperty("DiscountOrder")]
        public virtual ICollection<Order> OrderDiscount { get; set; } = new List<Order>();
    }
}
