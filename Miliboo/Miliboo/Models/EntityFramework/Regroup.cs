using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_j_regroup_rgp")]

public class Regroup
{
    private int regroupId;

    [Key]
    [Column("grp_id")]
    public int RegroupId
    {
        get { return regroupId; }
        set { regroupId = value; }
    }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductsRegroup")]
    public virtual Product ProductsNavigation { get; set; } = null!;

    [ForeignKey("GroupingId")]
    [InverseProperty("GroupingsRegroup")]
    public virtual Grouping GroupingsNavigation { get; set; } = null!;

}
