using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_deliverymethod_dlm")]

    public class DeliveryMethod
    {
        private int idDeliveryMethod;
        private string? descriptionDeliveryMethod;

        [Key]
        [Column("dlv_iddeliverymethod")]
        public int IdDeliveryMethod
        {
            get { return idDeliveryMethod; }
            set { idDeliveryMethod = value; }
        }


        [Column("dlv_descriptiondeliverymethod",TypeName ="Varchar(200)")]
        public string? DescriptionDeliveryMethod
        {
            get { return descriptionDeliveryMethod; }
            set { descriptionDeliveryMethod = value; }
        }



        [InverseProperty("DeliveryMethodOrder")]
        public virtual ICollection<Order> OrderDeliveryMethod { get; set; } = new List<Order>();

    }
}
