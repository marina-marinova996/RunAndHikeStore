namespace RunAndHikeStore.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using RunAndHikeStore.Data;
    using RunAndHikeStore.Data.Common;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Data.Repositories;
    using RunAndHikeStore.Data.Seeding;
    using RunAndHikeStore.Services;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Services.Data;
    using RunAndHikeStore.Services.Mapping;
    using RunAndHikeStore.Web.ModelBinders;
    using RunAndHikeStore.Web.ViewModels;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
               .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
            });

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                }).AddRazorRuntimeCompilation();

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);

            // Data repositories
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
