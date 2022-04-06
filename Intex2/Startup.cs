using Intex2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<CrashContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:CrashDbConnection"]); 
            });

            services.AddDbContext<AppIdentityDBContext>(options =>
            options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();

            //services.AddDbContext<CrashContext>(options =>
            //{
            //    options.UseSqlite(Configuration["ConnectionStrings:AppointmentDBConnection"]);
            //});

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<ICrashRepository, EFCrashRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<InferenceSession>(
                new InferenceSession("Models/crash_data_new.onnx")
            );

            services.AddRazorPages();

            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection(); // was commented out 
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            // Content-Security-Policy (CSP) HTTP header
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy",
                "default - src 'self'; style - src 'self'; img - src 'self'; script - src 'self'; base - uri 'self'; block - all - mixed - content 'self'; object - src 'none'; upgrade - insecure-requests 'self'; frame - src 'self'; font - src 'self'");
                await next();
            });


            //base-uri, block-all-mixed-content, object-src, upgrade-insecure-requests, frame-src, font-src
            //https://docs.microsoft.com/en-us/aspnet/core/blazor/security/content-security-policy?view=aspnetcore-3.1
            //"default-src 'self'; style-src 'self'; img-src 'self'; script-src 'self'; base-uri 'self'; block-all-mixed-content 'self'; object-src 'none'; upgrade-insecure-requests 'self'; frame-src 'self'; font-src 'self'");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute( //endpoints from pagination 
                    name: "Paging",
                    pattern: "page{pageNum}",
                    defaults: new { Controller = "Home", action = "AllCrashes" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub(); 
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index"); 
            });

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
