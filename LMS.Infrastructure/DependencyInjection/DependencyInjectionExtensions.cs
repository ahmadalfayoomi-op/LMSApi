using Infrastructure.Repositories.Course;
using Infrastructure.Repositories.User;
using LMS.Application.Interfaces.Auth;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Course;
using LMS.Application.Interfaces.User;
using LMS.Infrastructure.Repositories.Auth;
using LMS.Infrastructure.Repositories.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LMS.Infrastructure.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICateogryRepository, CateogryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAuthService, AuthService>();


            return services;
        }
    }

}
