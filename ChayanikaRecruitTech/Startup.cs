using ChayanikaRecruitTech.Models;
using CRT_Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChayanikaRecruitTech
{
    public class Startup
    {
        private readonly IWebHostEnvironment hostEnvironment;
        public string connectionstring = "";
        public string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration, IWebHostEnvironment hosting)
        {
            Configuration = configuration;
            hostEnvironment = hosting;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllersWithViews();
            services.AddControllers().AddXmlSerializerFormatters();
            hostEnvironment.EnvironmentName = "Development";
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder => {
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
          .WithOrigins("https://*.mydomain.com")
          .AllowAnyMethod()
          .AllowCredentials()
          .AllowAnyHeader()
          .Build();
                    });
 
            });
            if (hostEnvironment.IsDevelopment())
            {
                connectionstring = Configuration.GetConnectionString("DBConnection");
            }
            else if (hostEnvironment.IsProduction())
            {
                connectionstring = Configuration.GetConnectionString("DBConnection");
            }
            else if (hostEnvironment.IsEnvironment("Development"))
            {
                connectionstring = Configuration.GetConnectionString("DBConnection");
            }
            services.AddSession();
            services.AddDbContext<SKContextFile>(option => option.UseSqlServer(connectionstring), ServiceLifetime.Transient);
            ServiceDependentInjection.ServiceDependent(services);
          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseSession(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }
    }
}
