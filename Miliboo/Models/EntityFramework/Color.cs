using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Miliboo.Models.EntityFramework;

[Table("t_e_color_clr")]
public partial class Color
{
    private int colorId;
    private string? colorHexaCode;
    private string? colorName;

    public Color() { 
        ColorsProduct = new HashSet<Product>();
    }


    [Key]
    [Column("cl_id")]

    public int ColorId
    {
        get { return colorId; }
        set { colorId = value; }
    }

    [Column("clr_colorHexaCode", TypeName = "varchar(10)")]
    [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
    public string? ColorHexaCode
    {
        get { return colorHexaCode; }
        set { colorHexaCode = value; }
    }

    [Column("clr_colorName", TypeName = "varchar(50)")]
    public string? ColorName
    {
        get { return colorName; }
        set { colorName = value; }
    }

    [InverseProperty("ColorsNavigation")]
    public virtual ICollection<Product> ColorsProduct { get; set; } = new List<Product>();
}

