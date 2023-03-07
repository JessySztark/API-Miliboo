using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework {
    [Table("T_E_ADDRESS_ADR")]
    public class Address {
		private int adr_id;
		private String? adr_wording;
		private String? adr_postalcode;
		private String? adr_city;
		private float adr_longitude;
		private float adr_latitude;

        public Address() {
            Owners = new HashSet<Owning>();
        }

        [Key]
		[Column("adr_id")]
        public int AddressID {
            get { return adr_id; }
            set { adr_id = value; }
        }

        [Column("adr_wording", TypeName = "varchar")]
        public String? Wording {
            get { return adr_wording; }
            set { adr_wording = value; }
        }

        [Column("adr_postalcode", TypeName = "varchar")]
        public String? PostalCode {
            get { return adr_postalcode; }
            set { adr_postalcode = value; }
        }

        [Column("adr_city", TypeName = "varchar")]
        public String? City {
            get { return adr_city; }
            set { adr_city = value; }
        }

        [Column("adr_longitude", TypeName = "float")]
        public float Longitude {
            get { return adr_longitude; }
            set { adr_longitude = value; }
        }

        [Column("adr_latitude", TypeName="float")]
        public float Latitude {
			get { return adr_latitude; }
			set { adr_latitude = value; }
		}

        [ForeignKey("T_E_COUNTRY_CNT")]
        [InverseProperty("cnt_id")]
        public virtual Country CountryID { get; set; }

        public virtual ICollection<Owning> Owners { get; set; }
    }
}
