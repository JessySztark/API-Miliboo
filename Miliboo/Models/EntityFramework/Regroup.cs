using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_regroup_rgp")]

public class Regroup
{
    private int groupingId;

    [Key]
    [Column("grp_id")]
    public int RegroupId { get; set; }


    [ForeignKey("GroupingId")]
    [InverseProperty("GroupingsRegroup")]
    public virtual Grouping GroupingsNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductsRegroup")]
    public virtual Product ProductsNavigation { get; set; } = null!;
}
