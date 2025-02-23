using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace hello_world_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container, using AddControllers instead of AddMvc
            services.AddControllers();  // This is for API controllers
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
                app.UseHsts();
            }

            // Use HTTPS redirection and static files middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // UseRouting enables the routing middleware
            app.UseRouting();

            // UseAuthorization if you have authorization requirements
            app.UseAuthorization();

            // Map controllers to endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  // This maps API routes
            });
        }
    }
}
