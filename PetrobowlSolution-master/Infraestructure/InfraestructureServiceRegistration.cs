using Application.Models.Token;
using Application.Persistence;
using Infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class InfraestructureServiceRegistration
    {

        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            return services;
        }

    }
}
