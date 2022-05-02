using System;
using System.IO;
using System.Reflection;
using GamesDataCollector.Data;
using GamesDataCollector.Entities;
using GamesDataCollector.Services;
using GamesDataCollector.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GamesDataCollector
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
            //Configure DbCOntext
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("GamesDataConnection")).UseLazyLoadingProxies();
            });

            services.AddControllers();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IUsersService, UsersSerivce>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IScoreBoardService, ScoreBoardService>();

            //Repositories
            services.AddScoped<IRepository<User>, GDRepository<User>>();
            services.AddScoped<IRepository<Application>, GDRepository<Application>>();
            services.AddScoped<IRepository<GameData>, GDRepository<GameData>>();
            services.AddScoped<IRepository<Profile>, GDRepository<Profile>>();
            services.AddScoped<IRepository<Parent>, GDRepository<Parent>>();
            services.AddScoped<IRepository<Score>, GDRepository<Score>>();

            ConfigSwagger(services);

            //Configure AutoMapper
            ConfigAutoMapper(services);

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Games Data API");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action}",
                    defaults: new { action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "{controller}/{id?}");
            });

        }
        private void ConfigSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Games Data API", Version = "v1", Description = "REST APIs " });
                // Set the comments path for the Swagger JSON and UI.**
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigAutoMapper(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntity());
            });

            var mapper = config.CreateMapper();
            //register
            AutoMapperConfiguration.Init(config);
            services.AddSingleton(mapper);
        }
    }
}
