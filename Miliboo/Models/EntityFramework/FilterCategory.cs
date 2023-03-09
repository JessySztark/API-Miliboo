using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;


[Table("t_e_filterCategory_fca")]
public partial class FilterCategory
{
    private int filterCategoryId;
    private string? filterCategoryName;

    [Key]
    [Column("fca_id")]
    public int FilterCategoryId { get; set; }

    [Column("fca_filterCategoryName", TypeName = "varchar(50)")]
    public string? FilterCategoryName { get; set; }


    [InverseProperty("FiltersCategoryNavigation")]
    public virtual ICollection<Filter> FilterFiltersCategory { get; set; } = new List<Filter>();

    [InverseProperty("FiltersCategoryNavigation")]
    public virtual ICollection<AsFilter> AsFiltersFilterCategory { get; set; } = new List<AsFilter>();
}