namespace GenericKafkaProducer.WebApi.Startups
{
    using Behaviour;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class StartupE2E : Startup
    {
        public StartupE2E(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddFileDataProvider();
        }
    }
}