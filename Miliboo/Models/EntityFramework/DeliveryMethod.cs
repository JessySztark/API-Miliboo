using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_deliverymethod_dlm")]

    public class DeliveryMethod
    {
        private int idDeliveryMethod;
        private String? descriptionDeliveryMethod;
        /*private String? dlm_companyname;
        private bool dlm_shipmentathome;
        private String? dlm_title;
        private float? dlm_shippingprice;
        private String? dlm_image;*/

        [Key]
        [Column("dlm_id")]
        public int IdDeliveryMethod
        {
            get { return idDeliveryMethod; }
            set { idDeliveryMethod = value; }
        }

        [Column("dlm_description", TypeName = "text")]
        public String? Description
        {
            get { return descriptionDeliveryMethod; }
            set { descriptionDeliveryMethod = value; }
        }

        /*[Column("dlm_companyname", TypeName = "Varchar(50)")]
        public String? CompanyName {
            get { return dlm_companyname; }
            set { dlm_companyname = value; }
        }

        [Column("dlm_shipmentathome", TypeName = "boolean")]
        public bool ShipmentAtHome {
            get { return dlm_shipmentathome; }
            set { dlm_shipmentathome = value; }
        }

        [Column("dlm_title", TypeName = "Varchar(100)")]
        public String? Title {
            get { return dlm_title; }
            set { dlm_title = value; }
        }

        [Column("dlm_shippingprice", TypeName = "numeric")]
        public float? ShippingPrice {
            get { return dlm_shippingprice; }
            set { dlm_shippingprice = value; }
        }

        [Column("dlm_image", TypeName = "varchar(100)")]
        public String? Image {
            get { return dlm_image; }
            set { dlm_image = value; }
        }*/

        [InverseProperty("DeliveryMethodOrder")]
        public virtual ICollection<Order> OrderDeliveryMethod { get; set; } = new List<Order>();

    }
}
