using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_producttype_prt")]
public partial class ProductType
{
    private int productTypeId;
    private String productTypeName;
    private string? pTmaintenanceComment;

    public ProductType()
    {
        CommentsType = new HashSet<Comment>();
    }

    [Key]
    [Column("prt_id")]
    public int ProductTypeId
    {
        get { return productTypeId; }
        set { productTypeId = value; }
    }

    [Column("prt_productTypeName", TypeName = "varchar(100)")]
    [Required]
    public String ProductTypeName
    {
        get { return productTypeName; }
        set { productTypeName = value; }
    }

    [Column("prt_maintenanceCommentPT", TypeName = "varchar(500)")]
    public string? PTMaintenanceComment
    {
        get { return pTmaintenanceComment; }
        set { pTmaintenanceComment = value; }
    }

    [InverseProperty("ProductTypesNavigation")]
    public virtual ICollection<Product> ProductTypesProduct { get; set; } = new List<Product>();

    [InverseProperty("ProductTypesNavigation")]
    public virtual ICollection<AsAspect> AsAspectsProductType { get; set; } = new List<AsAspect>();

    [InverseProperty("TypeComments")]
    public virtual ICollection<Comment> CommentsType { get; set; } = new List<Comment>();
}
