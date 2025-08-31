using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("constr");
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(conn));
            return services;
        }
    }
}
