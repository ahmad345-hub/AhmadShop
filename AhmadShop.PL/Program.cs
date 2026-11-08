
using AhmadShop.BLL;
using AhmadShop.BLL.Settings;
using AhmadShop.DAL.Data;
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using AhmadShop.PL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace AhmadShop.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            builder.Services.AddScoped<IGenericRepositry<Product>,
                GenericRepository<Product>>();

            // Services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Seed Data
            builder.Services.AddScoped<ISeedData, RoleSeedData>();

            // Mapster
            MapsterConfig.RegisterMappings();

            var app = builder.Build();

            // Run Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var seeders = scope.ServiceProvider.GetServices<ISeedData>();

                foreach (var seeder in seeders)
                {
                    await seeder.DataSeed();
                }
            }

            // Configure HTTP pipeline
            app.UseRequestLocalization();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Serve uploaded images from wwwroot
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
