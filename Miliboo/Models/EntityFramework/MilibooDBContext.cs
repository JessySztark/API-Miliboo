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
                entity.HasCheckConstraint("Ck_creditcard_date", "crc_expirationdate > now()");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasCheckConstraint("Ck_order_date", "ord_date > now()");
            });

        }
        }
}
