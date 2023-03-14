using Microsoft.EntityFrameworkCore;
using Miliboo.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_j_asaspect_asa")]
public partial class AsAspect
{
    private string? aspectDescription;



    [Key]
    [Column("prt_id")]
    public int ProductTypeId { get; set; }

    [Key]
    [Column("tas_id")]
    public int TechnicalAspectId { get; set; }


    [Column("tas_technicalAspectName", TypeName = "varchar(50)")]
    public string? AspectDescription
    {
        get { return aspectDescription; }
        set { aspectDescription = value; }
    }

    [ForeignKey("TechnicalAspectId")]
    [InverseProperty("AsAspectsTechnicalAspect")]
    public virtual TechnicalAspect TechnicalAspectsNavigation { get; set; } = null!;

    [ForeignKey("ProductTypeId")]
    [InverseProperty("AsAspectsProductType")]
    public virtual ProductType ProductTypesNavigation { get; set; } = null!;
}
