using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_deliverymethod_dla")]
    public class DeliveryAdress
    {
		private int idDeliveryAdress;
        private int accountID;
        private string? favAdressName;

        [Key]
        [Column("dla_iddeliveryadress")]
        public int IdDeliveryAdress
		{
			get { return idDeliveryAdress; }
			set { idDeliveryAdress = value; }
		}

      

        [Column("dla_favadressname", TypeName = "varchar(50)")]
        public string? FavAdressName
        {
			get { return favAdressName; }
			set { favAdressName = value; }
		}

        [InverseProperty("DeliveryAdressOrder")]
        public virtual ICollection<Order> OrderDeliveryAdress { get; set; } = new List<Order>();



    }
}
