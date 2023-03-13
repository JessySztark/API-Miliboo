using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("T_E_COUNTRY_CNT")]
    public class Country
    {
        private int cnt_id;
        private String? cnt_wording;
        private String? cnt_phonecode;

        public Country() {
            AddressCountry = new HashSet<Address>();
        }

        [Key]
        [Column("cnt_id")]
        public int CountryID
        {
            get { return cnt_id; }
            set { cnt_id = value; }
        }

        [Column("cnt_wording", TypeName="varchar")]
        [MaxLength(100)]
        public String? Wording
        {
            get { return cnt_wording; }
            set { cnt_wording = value; }
        }

        [Column("cnt_phonecode", TypeName = "varchar")]
        [MaxLength(4)]
        public String? PhoneCode
        {
            get { return cnt_phonecode; }
            set { cnt_phonecode = value; }
        }

        [InverseProperty("CountryID")]
        public virtual ICollection<Address> AddressCountry { get; set; }
    }
}
