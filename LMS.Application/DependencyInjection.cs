using FluentValidation;
using LMS.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assemply = typeof(DependencyInjection).Assembly;
            services.AddValidatorsFromAssembly(assemply);


            services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()), assemply);

            return services;
        }
    }
}
