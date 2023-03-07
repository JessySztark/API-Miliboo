using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Miliboo.Models.EntityFramework;


[Table("t_e_product_prd")]
public partial class Product        //  /!\ Faire contraintes /!\   \\
{
    private int productId;
    private string? productName;
    private string? productDescription;
    private double productPrice;
    private double productDiscount;
    private int nbStockProduct;
    private int nbReservedProduct;

    [Key]
    [Column("prd_id")]
    public int ProductId { get; set; }

    [Column("prd_productName", TypeName = "varchar(100)")]
    public string? ProductName { get; set; }

    [Column("prd_productDescription", TypeName = "varchar(500)")]
    public string? ProductDescription { get; set; }

    [Column("prd_productPrice")]
    public double ProductPrice { get; set; }

    [Column("prd_productDiscount")]
    public double ProductDiscount { get; set; }

    [Column("prd_nbStockProduct")]
    public int NbStockProduct { get; set; }

    [Column("prd_nbReservedProduct")]
    public int NbReservedProduct { get; set; }


    [ForeignKey("ColorId")]
    [InverseProperty("ColorsProduct")]
    public virtual Color ColorsNavigation { get; set; } = null!;

    [ForeignKey("ProducTypetId")]
    [InverseProperty("ProductTypesProduct")]
    public virtual ProductType ProductTypesNavigation { get; set; } = null!;

    [ForeignKey("ProductCategoryId")]
    [InverseProperty("ProductCategoriesProduct")]
    public virtual ProductCategory ProductCategoriesNavigation { get; set; } = null!;

    [InverseProperty("ProductsNavigation")]
    public virtual ICollection<Regroup> ProductsRegroup { get; set; } = new List<Regroup>();

    [InverseProperty("ProductsNavigation")]
    public virtual ICollection<IsFiltered> ProductsIsFiltered { get; set; } = new List<IsFiltered>();
}

