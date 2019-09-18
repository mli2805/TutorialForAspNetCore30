using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;

namespace TheWorld
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;
        private IConfigurationRoot _config;

        public Startup(IHostEnvironment environment)
        {
            _environment = environment;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            if (_environment.IsDevelopment())
                services.AddScoped<IMailService, DebugMailService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MyDbContext>();
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<MyDbContext>();
            services.AddScoped<IMyDbRepository, MyDbRepository>();

            services.AddTransient<GeoCoordsService>();

            services.AddTransient<MyDbContextSeedData>();
            services.AddLogging(loggingBuilder => { loggingBuilder.AddDebug(); });

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc(options => options.EnableEndpointRouting = false).AddJsonOptions(config =>
            {
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

//            services.AddScoped<IUserStore<IdentityUser>, MyUserStore>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            MyDbContextSeedData seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            // app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "MyFirstRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                );
            });

            seeder.EnsureSeedData().Wait();
        }
    }
}
