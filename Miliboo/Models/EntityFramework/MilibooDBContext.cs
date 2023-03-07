using Microsoft.EntityFrameworkCore;

namespace Miliboo.Models.EntityFramework
{
    public class MilibooDBContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public MilibooDBContext()
        {
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
        }
        }
}
