using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_j_concerned_coc")]
public partial class Concerned
{
    private int concernedId;
    private int quantity;

    [Key]
    [Column("coc_id")]
    public int ConcernedId
    {
        get { return concernedId; }
        set { concernedId = value; }
    }

    [Column("coc_quantity")]
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }


    [ForeignKey("ProductId")]
    [InverseProperty("ProductsConcerned")]
    public virtual Product ProductsNavigation { get; set; } = null!;

    [ForeignKey("OrderID")]
    [InverseProperty("OrdersConcerned")]
    public virtual Order OrdersNavigation { get; set; } = null!;
}
