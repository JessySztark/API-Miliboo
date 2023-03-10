using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework;

[Table("t_j_concerned_coc")]
public class Concerned
{
    private int concernedId;
    private int quantity;


    [Key]
    [Column("coc_id")]
    public int ConcernedId { get; set; }
    
    [Column("coc_quantity")]
    public int Quantity { get; set; }


    [ForeignKey("ProductId")]
    [InverseProperty("ProductsConcerned")]
    public virtual Product ProductsNavigation { get; set; } = null!;

    [ForeignKey("OrderID")]
    [InverseProperty("OrdersConcerned")]
    public virtual Order OrdersNavigation { get; set; } = null!;
}
