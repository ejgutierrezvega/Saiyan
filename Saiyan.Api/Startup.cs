using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Saiyan.Repository;
using Saiyan.Mapper;
using Saiyan.Domain.Models;
using Entity = Saiyan.Domain.Entities;
using Saiyan.Components;
using Saiyan.Pluralization;

namespace Saiyan.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //Add mongo configuration.
            RepositoryConfig.Initialize();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            //Settings
            services.AddSingleton(typeof(IRepositorySettings), typeof(MongoSettings));

            //Services
            services.AddInstance(typeof(IPluralizationService), new EnglishPluralizationService());
            services.AddInstance(typeof(IIdEngine), new IdEngine()); //Object id engine.
            services.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>)); //Mongo DB Repository
            
            //Mappers
            services.AddInstance(typeof(IMapper<Person, Entity.Person>), new PersonMapper<Person, Entity.Person>());

            //Components
            services.AddSingleton(typeof(IComponent<Person>), typeof(PersonComponent));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
