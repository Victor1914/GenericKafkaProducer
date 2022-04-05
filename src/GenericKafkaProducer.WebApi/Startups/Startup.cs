namespace GenericKafkaProducer.WebApi.Startups
{
    using Behaviour;
    using Hubs;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http.Connections;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomHealthChecks()
                .AddSingalR()
                .AddMapping()
                .AddValidation()
                .AddConfigurations(Configuration)
                .AddKafkaProducers()
                .AddSerializers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<ProducerHub>("/producerhub", options =>
                    {
                        options.Transports = HttpTransportType.WebSockets;
                    });
                });
        }
    }
}