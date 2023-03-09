using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Miliboo.Models.EntityFramework
{
    public class MilibooDBContext : DbContext {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public MilibooDBContext() { }

        public MilibooDBContext(DbContextOptions<MilibooDBContext> options)
        : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.CardID).HasName("pk_creditcard");
                entity.HasCheckConstraint("Ck_creditcard_date", "crc_expirationdate > now()");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasCheckConstraint("Ck_order_date", "ord_date > now()");
            });

            modelBuilder.Entity<DeliveryAdress>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryAdress).HasName("pk_deliveryadress");
            });

            modelBuilder.Entity<DeliveryAdress>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryAdress).HasName("pk_deliveryadress");
            });

            modelBuilder.Entity<DeliveryMethod>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryMethod).HasName("pk_deliverymethod");
            });
           
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountID).HasName("pk_discount");
            });
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.Paymentmethodid).HasName("pk_paymentmethod");
            });
            modelBuilder.Entity<StateOrder>(entity =>
            {
                entity.HasKey(e => e.StateOrderID).HasName("pk_stateorder");
            });
            modelBuilder.Entity<Account>(entity => {

                entity.HasIndex(a => a.Mail)
                .IsUnique();
            });

            //
            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.ColorId).HasName("pk_color");
            });

            modelBuilder.Entity<FilterCategory>(entity =>
            {
                entity.HasKey(e => e.FilterCategoryId).HasName("pk_filtercategory");
            });

            modelBuilder.Entity<Filter>(entity =>
            {
                entity.HasKey(e => e.FilterId).HasName("pk_filter");
            });

            modelBuilder.Entity<IsFiltered>(entity =>
            {
                entity.HasKey(e => e.IsFilteredId).HasName("pk_isfiltered");
            });

            modelBuilder.Entity<AsFilter>(entity =>
            {
                entity.HasKey(e => new { e.FilterCategoryId, e.ProductCategoryId }).HasName("pk_asfilter");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryId).HasName("pk_productcategory");
            });

            modelBuilder.Entity<Grouping>(entity =>
            {
                entity.HasKey(e => e.GroupingId).HasName("pk_grouping");
            });

            modelBuilder.Entity<Regroup>(entity =>
            {
                entity.HasKey(e => e.RegroupId).HasName("pk_regroup");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.ProductTypetId).HasName("pk_producttype");
            });

            modelBuilder.Entity<TechnicalAspect>(entity =>
            {
                entity.HasKey(e => e.TechnicalAspectId).HasName("pk_technicalaspect");
            });

            modelBuilder.Entity<AsAspect>(entity =>
            {
                entity.HasKey(e => new { e.ProductTypeId, e.TechnicalAspectId }).HasName("pk_asaspect");
            });

            modelBuilder.Entity<Concerned>(entity =>
            {
                entity.HasKey(e => e.ConcernedId).HasName("pk_concerned");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("pk_product");
            });

        }
        }
}
