using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_productCategory_prc")]
public partial class ProductCategory
{
    private int productCategoryId;
    private string? productCategoryName;

    [Key]
    [Column("prc_id")]
    public int ProductCategoryId { get; set; }

    [Column("prc_productCategoryName", TypeName = "varchar(100)")]
    public string? ProductCategoryName { get; set; }


    [InverseProperty("ProductCategoriesNavigation")]
    public virtual ICollection<Product> ProductCategoriesProduct { get; set; } = new List<Product>();

    [InverseProperty("ProductCategoriesNavigation")]
    public virtual ICollection<AsFilter> AsFiltersProductCategory { get; set; } = new List<AsFilter>();


    // Relation parent-enfant
    public int? ParentCategoryId { get; set; }
    [ForeignKey("ParentCategoryId")]
    [InverseProperty("ChildCategories")]
    public virtual ProductCategory ParentCategory { get; set; }
    public virtual ICollection<ProductCategory> ChildCategories { get; set; } = new List<ProductCategory>();
}