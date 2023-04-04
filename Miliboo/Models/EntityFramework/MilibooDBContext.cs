using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Miliboo.Models.EntityFramework;

namespace Miliboo.Models.EntityFramework
{
    public class MilibooDBContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public MilibooDBContext() { }

        public MilibooDBContext(DbContextOptions<MilibooDBContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region sequences
            // ---------------------------------[SEQUENCES]--------------------------------- //
            modelBuilder.HasSequence<int>("SEQ_Account").StartsAt(101).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Address").StartsAt(251).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Color").StartsAt(23).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Comment").StartsAt(14).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_CompositeProduct").StartsAt(18).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Country").StartsAt(215).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_CreditCard").StartsAt(39).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_DeliveryAdress").StartsAt(35).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_DeliveryMethod").StartsAt(4).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Discount").StartsAt(6).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Filter").StartsAt(50).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_FilterCategory").StartsAt(37).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Grouping").StartsAt(4).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Order").StartsAt(38).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_PaymentMethod").StartsAt(4).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Photo").StartsAt(66).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Product").StartsAt(51).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_ProductCategory").StartsAt(39).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_ProductType").StartsAt(18).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_StateOrder").StartsAt(9).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_TechnicalAspect").StartsAt(20).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Concerned").StartsAt(39).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_IsFiltered").StartsAt(79).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SEQ_Regroup").StartsAt(20).IncrementsBy(1);

            modelBuilder.Entity<Account>(entity => {
                entity.Property(a => a.AccountID).HasDefaultValueSql("nextval(SEQ_Account)");
            });

            modelBuilder.Entity<Address>(entity => {
                entity.Property(a => a.AddressID).HasDefaultValueSql("nextval(SEQ_Address)");
            });

            modelBuilder.Entity<Color>(entity => {
                entity.Property(a => a.ColorId).HasDefaultValueSql("nextval(SEQ_Color)");
            });

            modelBuilder.Entity<Comment>(entity => {
                entity.Property(a => a.CommentID).HasDefaultValueSql("nextval(SEQ_Comment)");
            });

            modelBuilder.Entity<CompositeProduct>(entity => {
                entity.Property(a => a.CompositeproductID).HasDefaultValueSql("nextval(SEQ_CompositeproductID)");
            });

            modelBuilder.Entity<CreditCard>(entity => {
                entity.Property(a => a.CardID).HasDefaultValueSql("nextval(SEQ_CreditCard)");
            });

            modelBuilder.Entity<DeliveryAdress>(entity => {
                entity.Property(a => a.IdDeliveryAdress).HasDefaultValueSql("nextval(SEQ_DeliveryAdress)");
            });

            modelBuilder.Entity<DeliveryMethod>(entity => {
                entity.Property(a => a.IdDeliveryMethod).HasDefaultValueSql("nextval(SEQ_DeliveryMethod)");
            });

            modelBuilder.Entity<Discount>(entity => {
                entity.Property(a => a.DiscountID).HasDefaultValueSql("nextval(SEQ_Discount)");
            });

            modelBuilder.Entity<Filter>(entity => {
                entity.Property(a => a.FilterId).HasDefaultValueSql("nextval(SEQ_Filter)");
            });

            modelBuilder.Entity<FilterCategory>(entity => {
                entity.Property(a => a.FilterCategoryId).HasDefaultValueSql("nextval(SEQ_FilterCategory)");
            });

            modelBuilder.Entity<Grouping>(entity => {
                entity.Property(a => a.GroupingId).HasDefaultValueSql("nextval(SEQ_Grouping)");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.Property(a => a.OrderID).HasDefaultValueSql("nextval(SEQ_Order)");
            });

            modelBuilder.Entity<PaymentMethod>(entity => {
                entity.Property(a => a.Paymentmethodid).HasDefaultValueSql("nextval(SEQ_PaymentMethod)");
            });

            modelBuilder.Entity<Photo>(entity => {
                entity.Property(a => a.PhotoID).HasDefaultValueSql("nextval(SEQ_Photo)");
            });

            modelBuilder.Entity<Product>(entity => {
                entity.Property(a => a.ProductId).HasDefaultValueSql("nextval(SEQ_Product)");
            });

            modelBuilder.Entity<ProductCategory>(entity => {
                entity.Property(a => a.ProductCategoryId).HasDefaultValueSql("nextval(SEQ_ProductCategory)");
            });

            modelBuilder.Entity<ProductType>(entity => {
                entity.Property(a => a.ProductTypeId).HasDefaultValueSql("nextval(SEQ_ProductType)");
            });

            modelBuilder.Entity<StateOrder>(entity => {
                entity.Property(a => a.StateOrderID).HasDefaultValueSql("nextval(SEQ_StateOrder)");
            });

            modelBuilder.Entity<TechnicalAspect>(entity => {
                entity.Property(a => a.TechnicalAspectId).HasDefaultValueSql("nextval(SEQ_TechnicalAspect)");
            });

            modelBuilder.Entity<Concerned>(entity => {
                entity.Property(a => a.ConcernedId).HasDefaultValueSql("nextval(SEQ_Concerned)");
            });

            modelBuilder.Entity<IsFiltered>(entity => {
                entity.Property(a => a.IsFilteredId).HasDefaultValueSql("nextval(SEQ_IsFiltered)");
            });

            modelBuilder.Entity<Regroup>(entity => {
                entity.Property(a => a.RegroupId).HasDefaultValueSql("nextval(SEQ_Regroup)");
            });
            #endregion
            #region primary
            // ---------------------------------[PRIMARY KEY]--------------------------------- //

            // -----------[Credit Card]----------- //

            modelBuilder.Entity<CreditCard>(entity => {
                entity.HasKey(e => e.CardID).HasName("pk_creditcard");
                entity.HasCheckConstraint("Ck_creditcard_date", "crc_expirationdate > now()");
            });

            // -----------[Order]----------- //

            modelBuilder.Entity<Order>(entity => {
                entity.HasCheckConstraint("Ck_order_date", "ord_date > now()");
            });

            // -----------[Delivery Address]----------- //

            modelBuilder.Entity<DeliveryAdress>(entity => {
                entity.HasKey(e => e.IdDeliveryAdress).HasName("pk_deliveryadress");
            });

            // -----------[Delivery Method]----------- //

            modelBuilder.Entity<DeliveryMethod>(entity => {
                entity.HasKey(e => e.IdDeliveryMethod).HasName("pk_deliverymethod");
            });

            // -----------[Discount]----------- //

            modelBuilder.Entity<Discount>(entity => {
                entity.HasKey(e => e.DiscountID).HasName("pk_discount");
            });

            // -----------[Payment Method]----------- //

            modelBuilder.Entity<PaymentMethod>(entity => {
                entity.HasKey(e => e.Paymentmethodid).HasName("pk_paymentmethod");
            });

            // -----------[State Order]----------- //

            modelBuilder.Entity<StateOrder>(entity => {
                entity.HasKey(e => e.StateOrderID).HasName("pk_stateorder");
            });

            // -----------[Account]----------- //

            modelBuilder.Entity<Account>(entity => {
                entity.HasKey(a => a.AccountID).HasName("pk_accountid");
            });

            // -----------[Color]----------- //

            modelBuilder.Entity<Color>(entity => {
                entity.HasKey(e => e.ColorId).HasName("pk_color");
            });

            // -----------[Filter Category]----------- //

            modelBuilder.Entity<FilterCategory>(entity => {
                entity.HasKey(e => e.FilterCategoryId).HasName("pk_filtercategory");
            });

            // -----------[Filter]----------- //

            modelBuilder.Entity<Filter>(entity => {
                entity.HasKey(e => e.FilterId).HasName("pk_filter");
            });

            // -----------[Is Filtered]----------- //

            modelBuilder.Entity<IsFiltered>(entity => {
                entity.HasKey(e => e.IsFilteredId).HasName("pk_isfiltered");
            });

            // -----------[As Filter]----------- //

            modelBuilder.Entity<AsFilter>(entity => {
                entity.HasKey(e => new { e.FilterCategoryId, e.ProductCategoryId }).HasName("pk_asfilter");
            });

            // -----------[Product Category]----------- //

            modelBuilder.Entity<ProductCategory>(entity => {
                entity.HasKey(e => e.ProductCategoryId).HasName("pk_productcategory");
            });

            // -----------[Grouping]----------- //

            modelBuilder.Entity<Grouping>(entity => {
                entity.HasKey(e => e.GroupingId).HasName("pk_grouping");
            });

            // -----------[Regroup]----------- //

            modelBuilder.Entity<Regroup>(entity => {
                entity.HasKey(e => e.RegroupId).HasName("pk_regroup");
            });

            // -----------[Product Type]----------- //

            modelBuilder.Entity<ProductType>(entity => {
                entity.HasKey(e => e.ProductTypeId).HasName("pk_producttype");
            });

            // -----------[Technical Aspect]----------- //

            modelBuilder.Entity<TechnicalAspect>(entity => {
                entity.HasKey(e => e.TechnicalAspectId).HasName("pk_technicalaspect");
            });

            // -----------[As Aspect]----------- //

            modelBuilder.Entity<AsAspect>(entity => {
                entity.HasKey(e => new { e.ProductTypeId, e.TechnicalAspectId }).HasName("pk_asaspect");
            });

            // -----------[Concerned]----------- //

            modelBuilder.Entity<Concerned>(entity => {
                entity.HasKey(e => e.ConcernedId).HasName("pk_concerned");
            });

            // -----------[Product]----------- //

            modelBuilder.Entity<Product>(entity => {
                entity.HasKey(e => e.ProductId).HasName("pk_product");
            });


            // -----------[Address]----------- //

            modelBuilder.Entity<Address>(entity => {
                entity.HasKey(a => a.AddressID).HasName("pk_addressid");
            });

            // -----------[Comment]----------- //

            modelBuilder.Entity<Comment>(entity => {
                entity.HasKey(c => c.CommentID).HasName("pk_commentid");
            });

            // -----------[Country]----------- //

            modelBuilder.Entity<Country>(entity => {
                entity.HasKey(c => c.CountryID).HasName("pk_countryid");
            });

            // -----------[Owning]----------- //

            modelBuilder.Entity<Owning>(entity => {
                entity.HasKey(o => new { o.AddressID, o.AccountID }).HasName("pk_owning_addressid_accountid");
            });

            // -----------[Photo]----------- //

            modelBuilder.Entity<Photo>(entity => {
                entity.HasKey(p => p.PhotoID).HasName("pk_photo_photoid");
            });
            #endregion
            #region foreign
            // ---------------------------------[FOREIGN KEY]--------------------------------- //

            // -----------[Address]----------- //

            modelBuilder.Entity<Address>(entity => {
                entity.HasOne(n => n.CountryAdress)
                .WithMany(f => f.AddressCountry)
                .HasConstraintName("fk_adr_cnt");
            });

            // -----------[As Aspect]----------- //

            modelBuilder.Entity<AsAspect>(entity => {
                entity.HasOne(n => n.TechnicalAspectsNavigation)
                .WithMany(f => f.AsAspectsTechnicalAspect)
                .HasConstraintName("fk_techaspect_asaspect");
            });

            modelBuilder.Entity<AsAspect>(entity => {
                entity.HasOne(n => n.ProductTypesNavigation)
                .WithMany(f => f.AsAspectsProductType)
                .HasConstraintName("fk_producttype_asaspect");
            });

            // -----------[As Filter]----------- //

            modelBuilder.Entity<AsFilter>(entity => {
                entity.HasOne(n => n.FiltersCategoryNavigation)
                .WithMany(f => f.AsFiltersFilterCategory)
                .HasConstraintName("fk_filtercat_asfilter");
            });

            modelBuilder.Entity<AsFilter>(entity => {
                entity.HasOne(n => n.ProductCategoriesNavigation)
                .WithMany(f => f.AsFiltersProductCategory)
                .HasConstraintName("fk_productcat_asfilter");
            });

            // -----------[Comment]----------- //

            modelBuilder.Entity<Comment>(entity => {
                entity.HasOne(n => n.CommentsAccount)
                .WithMany(f => f.AccountComments)
                .HasConstraintName("fk_comments_account");
            });

            modelBuilder.Entity<Comment>(entity => {
                entity.HasOne(n => n.TypeComments)
                .WithMany(f => f.CommentsType)
                .HasConstraintName("fk_productcat_asfilter");
            });

            // -----------[Concerned]----------- //

            modelBuilder.Entity<Concerned>(entity => {
                entity.HasOne(n => n.ProductsNavigation)
                .WithMany(f => f.ProductsConcerned)
                .HasConstraintName("fk_product_concerned");
            });

            modelBuilder.Entity<Concerned>(entity => {
                entity.HasOne(n => n.OrdersNavigation)
                .WithMany(f => f.OrdersConcerned)
                .HasConstraintName("fk_order_concerned");
            });

            // -----------[Composite Product]----------- //

            modelBuilder.Entity<CompositeProduct>(entity => {
                entity.HasOne(n => n.ProductCompositeProduct)
                .WithMany(f => f.ProductsCompositeProduct)
                .HasConstraintName("fk_product_productcomposite");
            });

            // -----------[Filter]----------- //

            modelBuilder.Entity<Filter>(entity => {
                entity.HasOne(n => n.FiltersCategoryNavigation)
                .WithMany(f => f.FilterFiltersCategory)
                .HasConstraintName("fk_filter_filtercategory");
            });

            // -----------[Is Filter]----------- //

            modelBuilder.Entity<IsFiltered>(entity => {
                entity.HasOne(n => n.FiltersNavigation)
                .WithMany(f => f.FiltersIsFiltered)
                .HasConstraintName("fk_filter_isfiltered");
            });

            modelBuilder.Entity<IsFiltered>(entity => {
                entity.HasOne(n => n.ProductsNavigation)
                .WithMany(f => f.ProductsIsFiltered)
                .HasConstraintName("fk_product_isfiltered");
            });

            // -----------[Order]----------- //

            modelBuilder.Entity<Order>(entity => {
                entity.HasOne(n => n.CreditCardOrder)
                .WithMany(f => f.OrderCreditCard)
                .HasConstraintName("fk_order_creditcard");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasOne(n => n.DeliveryAdressOrder)
                .WithMany(f => f.OrderDeliveryAdress)
                .HasConstraintName("fk_order_deliveryaddress");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasOne(n => n.DeliveryMethodOrder)
                .WithMany(f => f.OrderDeliveryMethod)
                .HasConstraintName("fk_order_deliverymethod");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasOne(n => n.DiscountOrder)
                .WithMany(f => f.OrderDiscount)
                .HasConstraintName("fk_order_discount");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasOne(n => n.StateOrderOrder)
                .WithMany(f => f.OrderStateOrder)
                .HasConstraintName("fk_order_stateorder");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasOne(n => n.PaymentMethodOrder)
                .WithMany(f => f.OrderPaymentMethod)
                .HasConstraintName("fk_order_paymentmethod");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasOne(n => n.AccountOrder)
                .WithMany(f => f.OrderAccount)
                .HasConstraintName("fk_order_account");
            });

            // -----------[Photo]----------- //

            modelBuilder.Entity<Photo>(entity => {
                entity.HasOne(n => n.ProductPhoto)
                .WithMany(f => f.PhotoProduct)
                .HasConstraintName("fk_photo_product");
            });

            modelBuilder.Entity<Photo>(entity => {
                entity.HasOne(n => n.CommentPhoto)
                .WithMany(f => f.PhotoComment)
                .HasConstraintName("fk_photo_comment");
            });

            // -----------[Product]----------- //

            modelBuilder.Entity<Product>(entity => {
                entity.HasOne(n => n.ColorsNavigation)
                .WithMany(f => f.ColorsProduct)
                .HasConstraintName("fk_product_color");
            });

            modelBuilder.Entity<Product>(entity => {
                entity.HasOne(n => n.ProductTypesNavigation)
                .WithMany(f => f.ProductTypesProduct)
                .HasConstraintName("fk_product_typeproduct");
            });

            modelBuilder.Entity<Product>(entity => {
                entity.HasOne(n => n.ProductCategoriesNavigation)
                .WithMany(f => f.ProductCategoriesProduct)
                .HasConstraintName("fk_product_catproduct");
            });

            // -----------[Product Category]----------- //

            modelBuilder.Entity<ProductCategory>(entity => {
                entity.HasOne(n => n.ParentCategory)
                .WithMany(f => f.ChildCategories)
                .HasConstraintName("fk_productcat_productcat");
            });

            // -----------[Regroup]----------- //

            modelBuilder.Entity<Regroup>(entity => {
                entity.HasOne(n => n.GroupingsNavigation)
                .WithMany(f => f.GroupingsRegroup)
                .HasConstraintName("fk_regroup_grouping");
            });

            modelBuilder.Entity<Regroup>(entity => {
                entity.HasOne(n => n.ProductsNavigation)
                .WithMany(f => f.ProductsRegroup)
                .HasConstraintName("fk_regroup_product");
            });

            // -----------[Owning]----------- //

            modelBuilder.Entity<Owning>(entity => {
                entity.HasOne(n => n.OwnerAccount)
                .WithMany(f => f.Addresses)
                .HasConstraintName("fk_owning_account");
            });

            modelBuilder.Entity<Owning>(entity => {
                entity.HasOne(n => n.AddressOwned)
                .WithMany(f => f.Owners)
                .HasConstraintName("fk_owning_address");
            });
            #endregion
            #region unique
            // ---------------------------------[UNIQUE KEY]--------------------------------- //

            // -----------[Account]----------- //

            modelBuilder.Entity<Account>(entity => {
                entity.HasIndex(a => a.Mail)
                .IsUnique();
            });
            #endregion
            #region check
            // ---------------------------------[CHECK KEY]--------------------------------- //

            // -----------[Comment]----------- //

            modelBuilder.Entity<Comment>(entity => {
                entity.HasCheckConstraint("CK_COMMENT_MARK", "cmt_mark >=0 AND cmt_mark <=4");
            });

            // -----------[Photo]----------- //

            modelBuilder.Entity<Photo>(entity => {
                entity.HasCheckConstraint("CK_PHOTO_LINK", "pht_link::text ~ '^.*.(?:jpg|gif|png|webm|jpeg|ico|webp)$'::text");
            });

            // -----------[Address]----------- //

            modelBuilder.Entity<Address>(entity => {
                entity.HasCheckConstraint("CK_ADDR_POSTALCODE", "adr_postalcode::text ~ '^[0-9]{5}$'::text");
            });

            // -----------[CreditCard]----------- //

            modelBuilder.Entity<CreditCard>(entity => {
                entity.HasCheckConstraint("CK_CRC_CRYPTOGRAM", "crc_cryptogram::text ~ '^[0-9]{3}$'::text");
            });

            #endregion
            #region other
            // ---------------------------------[ELSE]--------------------------------- //

            // -----------[Comment]----------- //

            modelBuilder.Entity<Comment>(entity => {
                entity.Property(c => c.Date)
                .HasDefaultValue(DateTime.Now);
            });

            // -----------[Order]----------- //

            modelBuilder.Entity<Order>(entity => {
                entity.Property(o => o.Sms)
                .HasDefaultValue(false);
                entity.Property(d => d.DeliveryPrice)
                .HasDefaultValue(0);
                entity.Property(t => t.OrderDate)
                .HasDefaultValue(DateTime.Now);
            });

            #endregion
        }
        #region dbSet
        public DbSet<Miliboo.Models.EntityFramework.Product> Product { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Order> Order { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Address> Address { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.ProductCategory> ProductCategory { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.AsAspect> AsAspect { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.AsFilter> AsFilter { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.CompositeProduct> CompositeProduct { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Concerned> Concerned { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.DeliveryAdress> DeliveryAdress { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Discount> Discount { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Filter> Filter { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.FilterCategory> FilterCategory { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Owning> Owning { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.ProductType> ProductType { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.TechnicalAspect> TechnicalAspect { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Account> Account { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Photo> Photos { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Comment> Comments { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Country> Countries { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Color> Colors { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.CreditCard> CreditCards { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.StateOrder> StateOrders { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Grouping> Groupings { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.IsFiltered> IsFiltereds { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Miliboo.Models.EntityFramework.Regroup> Regroups { get; set; }
        #endregion
    }
}
