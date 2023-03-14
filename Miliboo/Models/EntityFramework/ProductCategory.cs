using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_productcategory_prc")]
public partial class ProductCategory
{
    private int productCategoryId;
    private string? productCategoryName;

    [Key]
    [Column("prc_id")]
    public int ProductCategoryId
    {
        get { return productCategoryId; }
        set { productCategoryId = value; }
    }

    [Column("prc_parentCategoryID", TypeName = "integer")]
    public int? ParentCategoryId { get; set; }







    [InverseProperty("ProductCategoriesNavigation")]
    public virtual ICollection<Product> ProductCategoriesProduct { get; set; } = new List<Product>();

    [InverseProperty("ProductCategoriesNavigation")]
    public virtual ICollection<AsFilter> AsFiltersProductCategory { get; set; } = new List<AsFilter>();

    [ForeignKey("ParentCategoryId")]
    [InverseProperty("ChildCategories")]
    public virtual ProductCategory ParentCategory { get; set; } = null!;
    public virtual ICollection<ProductCategory> ChildCategories { get; set; } = new List<ProductCategory>();

    [Column("prc_productCategoryName", TypeName = "varchar(100)")]
    public String? ProductCategoryName
    {
        get { return productCategoryName; }
        set { productCategoryName = value; }
    }
}