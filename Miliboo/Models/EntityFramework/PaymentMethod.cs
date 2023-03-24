using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_paymentmethod_pay")]
    public class PaymentMethod
    {

        private int paymentmethodid;
        private String methodName;

        [Key]
        [Column("pay_paymentmethodid")]
        public int Paymentmethodid
        {
            get { return paymentmethodid; }
            set { paymentmethodid = value; }
        }

        [Column("pay_methodname", TypeName = "varchar(100)")]
        [Required]
        public String MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }

        [InverseProperty("PaymentMethodOrder")]
        public virtual ICollection<Order> OrderPaymentMethod { get; set; } = new List<Order>();

    }
}
