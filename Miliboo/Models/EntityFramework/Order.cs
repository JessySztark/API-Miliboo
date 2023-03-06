using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Miliboo.Models.EntityFramework
{
    public class Order
    {

        [Key]
        [Column("ord_id")]
        public int OrderID { get; set; }



        public virtual CreditCard CreditCardOrder { get; set; } = null!;
        public virtual DeliveryAdress DeliveryAdressOrder { get; set; } = null!;
        public virtual DeliveryMethod DeliveryMethodOrder { get; set; } = null!;
        public virtual Discount DiscountOrder { get; set; } = null!;
        public virtual StateOrder StateOrderOrder { get; set; } = null!;
       

    }
}
