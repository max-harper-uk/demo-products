using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Demo.Products.Database.Configuration;
using Demo.Products.CQRS.Products;

namespace Demo.Products.CQRS.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddCQRSDependencies(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(GetProductsQuery).Assembly));
            services.AddDatabaseDependencies(configuration);
            return services;
        }
    }
}
