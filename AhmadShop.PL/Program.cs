using AhmadShop.BLL;
using AhmadShop.BLL.Settings;
using AhmadShop.DAL.Data;
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace AhmadShop.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // Localization
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(SupportedLanguage.Default);

                options.AddSupportedCultures(SupportedLanguage.Languages);
                options.AddSupportedUICultures(SupportedLanguage.Languages);

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DeafultConnection")));

            // Repositories
            builder.Services.AddScoped<IGenericRepositry<Category>,
                GenericRepository<Category>>();

            builder.Services.AddScoped<IGenericRepositry<Brand>,
                GenericRepository<Brand>>();

            // Services
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            // Mapster
            MapsterConfig.RegisterMappings();

            var app = builder.Build();

            // Configure HTTP pipeline
            app.UseRequestLocalization();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}