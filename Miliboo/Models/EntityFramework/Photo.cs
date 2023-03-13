using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Miliboo.Models.EntityFramework {
    public class Photo {
		private int pht_id;
        private String? pht_link;

        [Key]
        [Column("pht_id")]
        public int PhotoID {
			get { return pht_id; }
			set { pht_id = value; }
		}

        [Column("pht_id", TypeName = "varchar(200)")]
        public String? Link {
			get { return pht_link; }
			set { pht_link = value; }
		}

        [ForeignKey("ProductId")]
        [InverseProperty("PhotoProduct")]
        public virtual Product ProductPhoto { get; set; }

        [ForeignKey("CommentID")]
        [InverseProperty("PhotoComment")]
        public virtual Comment CommentPhoto { get; set; }
	}
}
