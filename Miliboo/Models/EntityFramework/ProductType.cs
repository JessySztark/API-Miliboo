using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_productType_prt")]
public partial class ProductType
{
    private int productTypeId;
    private string? productTypeName;
    private string? pTmaintenanceComment;

    public ProductType() {
        CommentsType = new HashSet<Comment>();
    }

    [Key]
    [Column("prt_id")]
    public int ProducTypetId { get; set; }

    [Column("prt_productTypeName", TypeName = "varchar(100)")]
    public string? ProductTypeName { get; set; }

    [Column("prt_maintenanceCommentPT", TypeName = "varchar(500)")]
    public string? PTMaintenanceComment { get; set; }


    [InverseProperty("ProductTypesNavigation")]
    public virtual ICollection<Product> ProductTypesProduct { get; set; } = new List<Product>();

    [InverseProperty("ProductTypesNavigation")]
    public virtual ICollection<AsAspect> AsAspectsProductType { get; set; } = new List<AsAspect>();

    [InverseProperty("TypeComments")]
    public virtual ICollection<Comment> CommentsType { get; set; } = new List<Comment>();
}
