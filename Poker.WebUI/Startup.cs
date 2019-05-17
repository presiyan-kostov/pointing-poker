using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Poker.WebUI
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
            services.AddCors();
            services.AddMvc();
            Registry.Register(services, Configuration.GetConnectionString("DefaultConnectionString"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(options => options.WithOrigins("*").AllowAnyMethod());
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "MVC", template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(name: "spa-fallback", defaults: 
                                           new
                                           {
                                               controller = "Home",
                                               action = "Index"
                                           });
            });
        }
    }
}