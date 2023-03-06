using Microsoft.EntityFrameworkCore;

namespace Miliboo.Models.EntityFramework {
    public class MilibooDBContext : DbContext {
        public static readonly ILoggerFactory Mylogs = LoggerFactory.Create(builder => builder.AddConsole());

        public MilibooDBContext() { }

        public MilibooDBContext(DbContextOptions<MilibooDBContext> options)
        : base(options) {
        }

        public virtual DbSet<Account> ACT { get; set; }
        public virtual DbSet<Address> ADR { get; set; }
        public virtual DbSet<Country> CNT { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Account>(entity => {

                entity.HasIndex(a => a.Mail)
                .IsUnique();
            });
        }

    }
}
