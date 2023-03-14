using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Miliboo.Models.EntityFramework
{



    [Table("t_e_photo_pht")]
    public class Photo
    {
        private int pht_id;
        private String? pht_link;

        [Key]
        [Column("pht_id")]
        public int PhotoID
        {
            get { return pht_id; }
            set { pht_id = value; }
        }

        [ForeignKey("ProductId")]
        [InverseProperty("PhotoProduct")]
        public virtual Product ProductPhoto { get; set; }

        [ForeignKey("CommentID")]
        [InverseProperty("PhotoComment")]
        public virtual Comment CommentPhoto { get; set; }

        [Column("pht_link", TypeName = "varchar(200)")]
        public String? Link
        {
            get { return pht_link; }
            set { pht_link = value; }
        }

    }
}
