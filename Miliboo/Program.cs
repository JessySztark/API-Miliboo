using Microsoft.EntityFrameworkCore;
using Miliboo.Models.DataManager;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;

namespace Miliboo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MilibooDBContext>(options =>
          options.UseNpgsql(builder.Configuration.GetConnectionString("MilibooDbContextRemote")));
            builder.Services.AddScoped<IDataRepository<Order>, OrderManager>();
            builder.Services.AddScoped<IDataRepository<Address>, AddressesManager>();
            builder.Services.AddScoped<IDataRepository<ProductCategory>, ProductCategoryManager>();
            builder.Services.AddScoped<IDataRepository<AsAspect>, AsAspectManager>();
            builder.Services.AddScoped<IDataRepository<AsFilter>, AsFilterManager>();
            builder.Services.AddScoped<IDataRepository<CompositeProduct>, CompositeProductManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}