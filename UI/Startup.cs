using BlogReact.Services.Implementations;
using BlogReact.Services.Interfaces;
using Data.Aurora.EF;
using Data.Aurora.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace BlogReact
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfigurationRoot ConfigurationRoot { get; set; }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var region = Configuration.GetValue<string>("AWS:Region");
            //new CredentialProfileStoreChain().TryGetAWSCredentials("default", out AWSCredentials awsCredentials);
            //Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", awsCredentials?.GetCredentials()?.AccessKey);
            //Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", awsCredentials?.GetCredentials()?.SecretKey);
            //Environment.SetEnvironmentVariable("AWS_REGION", region);

            services.AddDbContextPool<BlogContext>(options => 
            options.UseMySql(Configuration.GetConnectionString("AuroraConnection")
            //,mySqlOptions => mySqlOptions
            //                .ServerVersion(new Version(8, 0, 20), ServerType.MySql)
            //                .CharSetBehavior(CharSetBehavior.NeverAppend)
            ));


            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IBlogPostService, BlogPostService>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
            });
        }
    }
}
