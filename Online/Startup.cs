using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Online.DbStaff;
using Online.DbStaff.Model;
using Online.DbStaff.Repository;
using Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Online
{
    public class Startup
    {
        public const string AuthMethod = "CoockieAuth";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Online;Trusted_Connection=True;MultipleActiveResultSets=True;";
            services.AddDbContext<OnlineContext>(option => option.UseSqlServer(connectionString));

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "User.Auth";
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                });
            RegistrationMapper(services);

            RegistrationRepository(services);
            services.AddControllersWithViews();
        }
        private void RegistrationMapper(IServiceCollection services)
        {
            var configurationExpression = new MapperConfigurationExpression();

            configurationExpression.CreateMap<User, RegistrationViewModel>();
            configurationExpression.CreateMap<RegistrationViewModel, User>();

            var mapperConfiguration = new MapperConfiguration(configurationExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(s => mapper);
        }
        private void RegistrationRepository(IServiceCollection services)
        {
            services.AddScoped(s => new UserRepository(s.GetService<OnlineContext>()));
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
