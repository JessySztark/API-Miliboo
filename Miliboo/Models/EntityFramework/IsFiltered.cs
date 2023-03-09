using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_j_isFiltered_ift")]
public class IsFiltered
{
    private int isFilteredId;

    [Key]
    [Column("ift_id")]
    public int IsFilteredId { get; set; }


    [ForeignKey("ProductId")]
    [InverseProperty("ProductsIsFiltered")]
    public virtual Product ProductsNavigation { get; set; } = null!;

    [ForeignKey("FilterId")]
    [InverseProperty("FiltersIsFiltered")]
    public virtual Filter FiltersNavigation { get; set; } = null!;
}
