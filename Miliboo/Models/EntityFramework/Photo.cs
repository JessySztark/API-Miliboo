using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Miliboo.Models.EntityFramework {
    public class Photo {
		private int pht_id;
        private String? pht_link;

        public int PhotoID {
			get { return pht_id; }
			set { pht_id = value; }
		}

		public String Link {
			get { return pht_link; }
			set { pht_link = value; }
		}

        [ForeignKey("T_E_PRODUCT_PRD")]
        [InverseProperty("PhotoProduct")]
        public virtual Product ProductPhoto { get; set; }

        [ForeignKey("T_E_COMMENT_CMT")]
        [InverseProperty("PhotoComment")]
        public virtual Comment CommentPhoto { get; set; }
	}
}
