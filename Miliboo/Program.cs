using Microsoft.EntityFrameworkCore;
using Miliboo.Models.DataManager;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using MilibooAPI.Models.DataManager;

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
            builder.Services.AddScoped<IDataRepository<DeliveryAdress>, DeliveryAdressManager>();
            builder.Services.AddScoped<IDataRepository<Discount>, DiscountManager>();
            builder.Services.AddScoped<IDataRepository<Filter>, FilterManager>();
            builder.Services.AddScoped<IDataRepository<FilterCategory>, FilterCategoryManager>();
            builder.Services.AddScoped<IDataRepository<Owning>, OwningManager>();
            builder.Services.AddScoped<IDataRepository<ProductCategory>, ProductCategoryManager>();
            builder.Services.AddScoped<IDataRepository<ProductType>, ProductTypeManager>();
            builder.Services.AddScoped<IDataRepository<TechnicalAspect>, TechnicalAspectManager>();
            builder.Services.AddScoped<IDataRepository<Product>, ProductManager>();
            builder.Services.AddScoped<IDataRepository<Account>, AccountManager>();
            builder.Services.AddScoped<IDataRepository<Photo>, PhotoManager>();
            builder.Services.AddScoped<IDataRepository<Comment>, CommentManager>();
            builder.Services.AddScoped<IDataRepository<Color>, ColorManager>();
            builder.Services.AddScoped<IDataRepository<DeliveryMethod>, DeliveryMethodManager>();
            builder.Services.AddScoped<IDataRepository<StateOrder>, StateOrderManager>();
            builder.Services.AddScoped<IDataRepository<Grouping>, GroupingManager>();
            builder.Services.AddScoped<IDataRepository<IsFiltered>, IsFilteredManager>();
            builder.Services.AddScoped<IDataRepository<PaymentMethod>, PaymentMethodManager>();
            builder.Services.AddScoped<IDataRepository<PaymentMethod>, PaymentMethodManager>();
            builder.Services.AddScoped<IDataRepository<Regroup>, RegroupManager>();
            // builder.Services.AddScoped<IDataRepository<Country>, CountryManager>();

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