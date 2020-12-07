namespace EspartoWorld.Web
{
    using System.Globalization;
    using System.Reflection;

    using EspartoWorld.Data;
    using EspartoWorld.Data.Common;
    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Data.Repositories;
    using EspartoWorld.Data.Seeding;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Services.Mapping;
    using EspartoWorld.Services.Messaging;
    using EspartoWorld.Services.YouTube;
    using EspartoWorld.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Multilanguage
            // services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("es-ES"),
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            // In class IdentityOptionsProvider we switch off options that we dont want to use for examle email confirmation or password must have symbols and numbers and letters
            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>() // Should be here if we need to use roles
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            // with this service we can use Memory Cache
            services.AddMemoryCache();

            services.AddControllersWithViews(
                options =>
                    {
                        // this option puts in all actions attribute ValidateAntiforgeryToken and protect us for CSRF Attacks(is not possible from other forms that are not ours to send us data)
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddSingleton(this.configuration);

            // this service is for use DistributedCache
            services.AddDistributedSqlServerCache(
                options =>
                {
                    options.ConnectionString = this.configuration.GetConnectionString("DefaultConnection");
                    options.SchemaName = "dbo";
                    options.TableName = "YouTubeEspartoSearchCache";
                });
            services.AddSession();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            // services.AddTransient<IEmailSender, NullMessageSender>();
            // services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<IEmailSender>(x => new MailKitEmailSender(this.configuration["EmailSender:Email"], this.configuration["EmailSender:Password"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<IVideosService, VideosService>();
            services.AddTransient<IExposicionItemService, ExposicionItemService>();
            services.AddSingleton<IYouTubeDataService>(x => new YouTubeDataService(this.configuration["YouTube:ApiKey"]));
            services.AddTransient<IContactFormService, ContactFormService>();
            services.AddTransient<IManufacturersService, ManufacturersService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IShoppingCartsService, ShoppingCartsService>();
            services.AddTransient<IVotesService, VotesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
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

            // Multilanguage
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            // UseAuthentication and UseAuthorization must be between UseRouting and UseEndpoints
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
