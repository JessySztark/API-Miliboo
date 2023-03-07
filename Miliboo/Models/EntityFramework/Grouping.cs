using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_grouping_grp")]

public partial class Grouping
{
    private int groupingId;
    private string? groupingName;

    [Key]
    [Column("grp_id")]
    public int GroupingId { get; set; }

    [Column("grp_groupingName", TypeName = "varchar(50)")]
    public string? GroupingName { get; set; }


    [InverseProperty("GroupingsNavigation")]
    public virtual ICollection<Grouping> GroupingsRegroup { get; set; } = new List<Grouping>();
}
