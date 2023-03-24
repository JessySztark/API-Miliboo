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
    public int GroupingId
    {
        get { return groupingId; }
        set { groupingId = value; }
    }

    [Column("grp_groupingName", TypeName = "varchar(50)")]
    public string? GroupingName
    {
        get { return groupingName; }
        set { groupingName = value; }
    }

    [InverseProperty("GroupingsNavigation")]
    public virtual ICollection<Regroup> GroupingsRegroup { get; set; } = new List<Regroup>();
}
