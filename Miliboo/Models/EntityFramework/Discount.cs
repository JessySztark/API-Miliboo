using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_discount_dsc")]

    public class Discount
    {
        private int discountID;
        private string? discountName;
        private bool isActive;
        private double discountValue;


        [Key]
        [Column("dsc_discountid")]
        public int DiscountID
        {
            get { return discountID; }
            set { discountID = value; }
        }

        [Column("dsc_discountname")]
        public string? DiscountName
        {
            get { return discountName; }
            set { discountName = value; }
        }

        [Column("dsc_isactive")]
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [Column("dsc_value")]
        public double DiscountValue
        {
            get { return discountValue; }
            set { discountValue = value; }
        }

        [InverseProperty("DiscountOrder")]
        public virtual ICollection<Order> OrderDiscount { get; set; } = new List<Order>();
    }
}
