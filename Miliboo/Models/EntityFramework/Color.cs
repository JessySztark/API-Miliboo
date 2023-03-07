using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_color_clr")]
public partial class Color        //  /!\ Faire contraintes /!\   \\
{
    private int colorId;
    private string? colorName;
    private string? colorHexaCode;


    [Key]
    [Column("clr_id")]
    public int ColorId { get; set; }

    [Column("clr_colorName", TypeName ="varchar(50)")]
    public string? ColorName { get; set; }

    [Column("clr_colorHexaCode", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? ColorHexaCode { get; set; }


    [InverseProperty("ColorsNavigation")]
    public virtual ICollection<Product> ColorsProduct { get; set; } = new List<Product>();
}

