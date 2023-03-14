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

            // -----------[Payement Method]----------- //

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

            // ---------------------------------[UNIQUE & CHECK KEY]--------------------------------- //

            // -----------[Account]----------- //

            modelBuilder.Entity<Account>(entity => {

                entity.HasIndex(a => a.Mail)
                .IsUnique();
            });

            // ---------------------------------[ELSE]--------------------------------- //

            // -----------[Comment]----------- //

        }

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
    }
}
