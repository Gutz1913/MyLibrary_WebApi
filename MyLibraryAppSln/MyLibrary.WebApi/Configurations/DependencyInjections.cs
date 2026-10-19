using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Repositories.Implementations;

namespace MyLibrary.WebApi.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection SetDependencyInjection(this IServiceCollection services)
    {
        #region Repositories Injection
        services.AddScoped<IUserRepository, UserRepository>();

        #endregion

        #region Services Injection
        services.AddScoped<IUserService, UserService>();

        #endregion

        return services;
    }
}

