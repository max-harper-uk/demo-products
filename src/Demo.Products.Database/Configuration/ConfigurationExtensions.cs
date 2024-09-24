using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Products.Database.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(ProductsDb));
            if(connectionString == null) 
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddDbContext<ProductsDb>(opt => opt.UseMySQL(connectionString));

            return services;
        }
    }
}
