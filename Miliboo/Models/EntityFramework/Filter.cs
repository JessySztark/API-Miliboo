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
    public int FilterId
    {
        get { return filterId; }
        set { filterId = value; }
    }

    [ForeignKey("FilterCategoryId")]
    [InverseProperty("FilterFiltersCategory")]
    public virtual FilterCategory FiltersCategoryNavigation { get; set; } = null!;


    [Column("flt_filterName", TypeName = "varchar(50)")]
    public string? FilterName
    {
        get { return filterName; }
        set { filterName = value; }
    }



    [InverseProperty("FiltersNavigation")]
    public virtual ICollection<IsFiltered> FiltersIsFiltered { get; set; } = new List<IsFiltered>();
}