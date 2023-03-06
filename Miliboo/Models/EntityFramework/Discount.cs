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
        public int DiscountID { get; set; }

        [Column("dsc_discountname")]
        public string? DiscountName { get; set; }

        [Column("dsc_isactive")]
        public bool IsActive { get; set; }

        [Column("dsc_isactive")]
        public double DiscountValue { get; set; }


        public virtual ICollection<Order> OrderDiscount { get; set; } = new List<Order>();
    }
}
