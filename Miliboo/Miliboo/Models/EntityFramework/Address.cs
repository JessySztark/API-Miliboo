﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_address_adr")]
    public class Address
    {
        private int adr_id;
        private String adr_wording;
        private String adr_postalcode;
        private String adr_city;
        private float? adr_longitude;
        private float? adr_latitude;
        private int countryID;

        public Address()
        {
            Owners = new HashSet<Owning>();
        }

        [Key]
        [Column("adr_id")]
        public int AddressID
        {
            get { return adr_id; }
            set { adr_id = value; }
        }

        [Column("cnt_id")]
        public int CountryID
        {
            get { return countryID; }
            set { countryID = value; }
        }

        [Column("adr_wording", TypeName = "varchar")]
        [Required]
        public String Wording
        {
            get { return adr_wording; }
            set { adr_wording = value; }
        }

        [Column("adr_postalcode", TypeName = "varchar")]
        [Required]
        public String PostalCode
        {
            get { return adr_postalcode; }
            set { adr_postalcode = value; }
        }

        [Column("adr_city", TypeName = "varchar")]
        [Required]
        public String City
        {
            get { return adr_city; }
            set { adr_city = value; }
        }

        [Column("adr_longitude", TypeName = "numeric")]
        public float? Longitude
        {
            get { return adr_longitude; }
            set { adr_longitude = value; }
        }

        [Column("adr_latitude", TypeName = "numeric")]
        public float? Latitude
        {
            get { return adr_latitude; }
            set { adr_latitude = value; }
        }

        [ForeignKey("CountryID")]
        [InverseProperty("AddressCountry")]
        public virtual Country CountryAdress { get; set; }

        [InverseProperty("AddressOwned")]
        public virtual ICollection<Owning> Owners { get; set; }
    }
}
