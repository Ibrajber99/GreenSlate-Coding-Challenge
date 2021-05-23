using GreenSlate_Coding_Challenge.BusinessLogic;
using GreenSlate_Coding_Challenge.Data.Entities.Drinks;
using GreenSlate_Coding_Challenge.Data.Repositories;
using GreenSlate_Coding_Challenge.Models;
using GreenSlate_Coding_Challenge.Models.InputModels;
using GreenSlate_Coding_Challenge.Models.VendingMachine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GreenSlate_Coding_Challenge
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
            //Sepcifying our Dependencies 

            services.AddScoped<MachineBase<DrinkBase>, DrinksVendingMachine>();

            services.AddScoped<DrinksVendingMachine>();

            services.AddScoped<InputViewModel>();

            services.AddScoped<UserTransaction>();

            services.AddScoped<IBusinessService, BusinessService>();

            services.AddSingleton<IDrinkRepository, DrinkRepository>();

            services.AddSingleton<ICoinRepository, CoinRepository>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=DrinksMachine}/{id?}");
            });
        }
    }
}
