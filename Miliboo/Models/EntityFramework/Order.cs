using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_order_ord")]
    public class Order
    {
        private string? orderName;
        private int orderID;
        private int cardID;
        private int idDeliveryAdress;
        private int idDeliveryMethod;
        private int discountID;
        private int stateOrderID;
        private int accountID;
        private int paymentmethodid;
        private string? orderFirstName;
        private string? phoneOrder;
        private string? cellPhone;
        private string? company;
        private string? adressAdditional;
        private string? orderInstructions;
        private DateTime orderDate;
        private float deliveryPrice;
        private bool sms;


        [Key]
        [Column("ord_id")]
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        [Key]
        [Column("crc_cardid")]
        public int CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }

        [Key]
        [Column("dla_iddeliveryadress")]
        public int IdDeliveryAdress
        {
            get { return idDeliveryAdress; }
            set { idDeliveryAdress = value; }
        }
        [Key]
        [Column("dlv_iddeliverymethod")]
        public int IdDeliveryMethod
        {
            get { return idDeliveryMethod; }
            set { idDeliveryMethod = value; }
        }

        [Key]
        [Column("dsc_discountid")]
        public int DiscountID
        {
            get { return discountID; }
            set { discountID = value; }
        }
        [Key]
        [Column("sto_stateorderid")]
        public int StateOrderID
        {
            get { return stateOrderID; }
            set { stateOrderID = value; }
        }

        [Key]
        [Column("act_id")]
        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        [Key]
        [Column("pay_paymentmethodid")]
        public int Paymentmethodid
        {
            get { return paymentmethodid; }
            set { paymentmethodid = value; }
        }


        [Column("ord_name", TypeName ="varchar(50)")]
        public string? OrderName
        {
            get { return orderName; }
            set { orderName = value; }
        }

        [Column("ord_firstname", TypeName = "varchar(50)")]
        public string? OrderFirstName
        {
            get { return orderFirstName; }
            set { orderFirstName = value; }
        }

        [Column("ord_phone", TypeName = "varchar(20)")]
        public string? PhoneOrder
        {
            get { return phoneOrder; }
            set { phoneOrder = value; }
        }

        [Column("ord_cellphone", TypeName = "varchar(20)")]
        public string? CellPhone
        {
            get { return cellPhone; }
            set { cellPhone = value; }
        }

        [Column("ord_company", TypeName = "varchar(50)")]
        public string? Company
        {
            get { return company; }
            set { company = value; }
        }

        [Column("ord_adressadditional", TypeName = "varchar(200)")]
        public string? AdressAdditional
        {
            get { return adressAdditional; }
            set { adressAdditional = value; }
        }

        [Column("ord_instructions", TypeName = "varchar(200)")]
        public string? OrderInstructions
        {
            get { return orderInstructions; }
            set { orderInstructions = value; }
        }

        [Column("ord_date", TypeName = "varchar(200)")]
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        [Column("ord_deliveryprice")]
        public float DeliveryPrice
        {
            get { return deliveryPrice; }
            set { deliveryPrice = value; }
        }

        [Column("ord_sms")]
        public bool Sms
        {
            get { return sms; }
            set { sms = value; }
        }


        [ForeignKey("CardID")]
        [InverseProperty("OrderCreditCard")]
        public virtual CreditCard CreditCardOrder { get; set; } = null!;


        [ForeignKey("IdDeliveryAdress")]
        [InverseProperty("OrderDeliveryAdress")]
        public virtual DeliveryAdress DeliveryAdressOrder { get; set; } = null!;

        [ForeignKey("IdDeliveryMethod")]
        [InverseProperty("OrderDeliveryMethod")]
        public virtual DeliveryMethod DeliveryMethodOrder { get; set; } = null!;

        [ForeignKey("DiscountID")]
        [InverseProperty("OrderDiscount")]
        public virtual Discount DiscountOrder { get; set; } = null!;

        [ForeignKey("stateOrderID")]
        [InverseProperty("OrderStateOrder")]
        public virtual StateOrder StateOrderOrder { get; set; } = null!;


        [ForeignKey("Paymentmethodid")]
        [InverseProperty("OrderPaymentMethod")]
        public virtual PaymentMethod PaymentMethodOrder { get; set; } = null!;

        [ForeignKey("AccountID")]
        [InverseProperty("OrderAccount")]
        public virtual Account AccountOrder { get; set; } = null!;

        [InverseProperty("OrdersNavigation")]
        public virtual ICollection<Concerned> OrdersConcerned { get; set; } = new List<Concerned>();

    }
}
