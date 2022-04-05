namespace GenericKafkaProducer.WebApi.Startups
{
    using Behaviour;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class StartupPerformance : Startup
    {
        public StartupPerformance(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddMockDataProvider();
        }
    }
}