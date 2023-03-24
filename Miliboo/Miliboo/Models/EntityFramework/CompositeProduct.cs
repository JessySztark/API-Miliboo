﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{


    [Table("t_e_compositeproduct_cpr")]

    public class CompositeProduct
    {
        private int compositeID;
        private int productId;
        private int compositeproduct;
        private string? compositeDescription;

        [Key]
        [Column("cpr_id")]
        public int CompositeID
        {
            get { return compositeID; }
            set { compositeID = value; }
        }

        [Column("prd_id")]
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        [Column("cpr_compositeproductid")]
        public int CompositeproductID
        {
            get { return compositeproduct; }
            set { compositeproduct = value; }
        }

        [Column("cpr_compositedescription")]
        public string? CompositeDescription
        {
            get { return compositeDescription; }
            set { compositeDescription = value; }
        }

        [ForeignKey("ProductId")]
        [InverseProperty("ProductsCompositeProduct")]
        public virtual Product ProductCompositeProduct { get; set; } = null!;

    }
}
