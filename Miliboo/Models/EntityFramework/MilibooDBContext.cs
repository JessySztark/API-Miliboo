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
            // -----------[Credit Card]----------- //

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.CardID).HasName("pk_creditcard");
                entity.HasCheckConstraint("Ck_creditcard_date", "crc_expirationdate > now()");
            });

            // -----------[Order]----------- //

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasCheckConstraint("Ck_order_date", "ord_date > now()");
            });

            // -----------[Delivery Address]----------- //

            modelBuilder.Entity<DeliveryAdress>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryAdress).HasName("pk_deliveryadress");
            });

            modelBuilder.Entity<DeliveryAdress>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryAdress).HasName("pk_deliveryadress");
            });

            // -----------[Delivery Method]----------- //

            modelBuilder.Entity<DeliveryMethod>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryMethod).HasName("pk_deliverymethod");
            });

            // -----------[Discount]----------- //

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountID).HasName("pk_discount");
            });

            // -----------[Payement Method]----------- //

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.Paymentmethodid).HasName("pk_paymentmethod");
            });

            // -----------[State Order]----------- //

            modelBuilder.Entity<StateOrder>(entity =>
            {
                entity.HasKey(e => e.StateOrderID).HasName("pk_stateorder");
            });

            // -----------[Account]----------- //

            modelBuilder.Entity<Account>(entity => {
                entity.HasKey(a => a.AccountID).HasName("pk_accountid");
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
                entity.HasKey(o => new { o.IDAddress, o.IDAccount}).HasName("pk_owning_addressid_accountid");
            });

            // -----------[Photo]----------- //

            modelBuilder.Entity<Photo>(entity => {
                entity.HasKey(p => p.PhotoID).HasName("pk_owning_addressid_accountid");
            });

        }
    }
}
