using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;


[Table("t_e_filter_flt")]
public partial class Filter
{
    private int filterId;
    private string? filterName;

    [Key]
    [Column("flt_id")]
    public int FilterId { get; set; }

    [Column("flt_filterName", TypeName = "varchar(50)")]
    public string? FilterName { get; set; }

    [ForeignKey("FilterCategoryId")]
    [InverseProperty("FilterFiltersCategory")]
    public virtual FilterCategory FiltersCategoryNavigation { get; set; } = null!;

    [InverseProperty("FiltersNavigation")]
    public virtual ICollection<IsFiltered> FiltersIsFiltered { get; set; } = new List<IsFiltered>();
}