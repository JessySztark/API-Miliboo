using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_j_asfilter_aft")]
public partial class AsFilter
{
    [Key]
    [Column("fca_id")]
    public int FilterCategoryId { get; set; }

    [Key]
    [Column("prc_id")]
    public int ProductCategoryId { get; set; }

    [ForeignKey("FilterCategoryId")]
    [InverseProperty("AsFiltersFilterCategory")]
    public virtual FilterCategory FiltersCategoryNavigation { get; set; } = null!;

    [ForeignKey("ProductCategoryId")]
    [InverseProperty("AsFiltersProductCategory")]
    public virtual ProductCategory ProductCategoriesNavigation { get; set; } = null!;
}
