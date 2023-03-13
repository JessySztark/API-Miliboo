using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_technicalAspect_tas")]
public partial class TechnicalAspect
{
    private int technicalAspectId;
    private string? technicalAspectName;

    [Key]
    [Column("tas_id")]
    public int TechnicalAspectId { get; set; }

    [Column("tas_technicalAspectName", TypeName = "varchar(500)")]
    public string? TechnicalAspectName { get; set; }

    [InverseProperty("TechnicalAspectsNavigation")]
    public virtual ICollection<AsAspect> AsAspectsTechnicalAspect { get; set; } = new List<AsAspect>();
}
