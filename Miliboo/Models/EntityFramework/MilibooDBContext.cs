using Microsoft.EntityFrameworkCore;

namespace Miliboo.Models.EntityFramework {
    public class MilibooDBContext : DbContext {
        public static readonly ILoggerFactory Mylogs = LoggerFactory.Create(builder => builder.AddConsole());

        public MilibooDBContext() { }

        public MilibooDBContext(DbContextOptions<MilibooDBContext> options)
        : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasCheckConstraint("Ck_creditcard_date", "crc_expirationdate > now()");
            });
            modelBuilder.Entity<Account>(entity => {

                entity.HasIndex(a => a.Mail)
                .IsUnique();
            });
        }


        }
}
