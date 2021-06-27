using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CardsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddCors(c =>
                c.AddPolicy("AllowOrigin", o => o.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
            );

            services.AddControllersWithViews()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = 
                    ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(o => 
                    o.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(o => o.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
