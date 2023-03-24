using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_j_isfiltered_ift")]
public class IsFiltered
{
    private int isFilteredId;

    [Key]
    [Column("ift_id")]
    public int IsFilteredId
    {
        get { return isFilteredId; }
        set { isFilteredId = value; }
    }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductsIsFiltered")]
    public virtual Product ProductsNavigation { get; set; } = null!;

    [ForeignKey("FilterId")]
    [InverseProperty("FiltersIsFiltered")]
    public virtual Filter FiltersNavigation { get; set; } = null!;
}
