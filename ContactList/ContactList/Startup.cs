using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ContactList.Configuration;
using ContactList.Filters;
using ContactList.Infrastructure;
using ContactList.Core.Repositories;
using ContactList.Infrastructure.Repositories;
using ContactList.Core.Services;

namespace ContactList
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
            services.AddControllersWithViews();

            services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DatabaseContext")));

            services.Configure<HostingOptions>(
                Configuration.GetSection("Hosting"));

            // setup our services
            services.AddTransient<IContactListRepository, ContactListRepository>();
            services.AddTransient<IContactListService, ContactListService>();

            services.AddTransient<ExecutionMonitorFilter>();

            /*
            // Uncomment if you want to register the filter globally
            services.AddMvc(options =>
            {
                options.Filters.AddService<ExecutionMonitorFilter>();
            });
            */
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ContactList}/{action=Index}/{id?}");
            });
        }
    }
}
