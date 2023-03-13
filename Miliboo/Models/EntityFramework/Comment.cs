using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework {
	[Table("T_E_COMMENT_CMT")]
    public class Comment {
		private int cmt_id;
        private String? cmt_title;
        private int? cmt_mark;
        private String? cmt_description;
        private DateTime cmt_date;

		[Key]
        [Column("cmt_id")]
        public int CommentID {
			get { return cmt_id; }
			set { cmt_id = value; }
		}

        [Column("cmt_title", TypeName="varchar")]
        public String? Title {
			get { return cmt_title; }
			set { cmt_title = value; }
		}
        [Column("cmt_mark")]
		[Range(1,4)]
        public int? Mark {
			get { return cmt_mark; }
			set { cmt_mark = value; }
		}
        [Column("cmt_description", TypeName = "varchar")]
        public String? Description {
			get { return cmt_description; }
			set { cmt_description = value; }
		}
        [Column("cmt_date", TypeName = "date")]
		[DefaultValue("now()")]
        public DateTime Date {
			get { return cmt_date; }
			set { cmt_date = value; }
		}

        [ForeignKey("AccountID")]
        [InverseProperty("AccountComments")]
        public virtual Account CommentsAccount { get; set; }

        [ForeignKey("ProductTypetId")]
        [InverseProperty("CommentsType")]
        public virtual ProductType TypeComments { get; set; }

        [InverseProperty("CommentPhoto")]
        public virtual ICollection<Photo> PhotoComment { get; set; } = new List<Photo>();
    }
}
