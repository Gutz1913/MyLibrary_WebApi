using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Repositories.Implementations;
using MyLibrary.WebApi.Services.Contracts;
using MyLibrary.WebApi.Services.Implementations;

namespace MyLibrary.WebApi.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection SetDependencyInjection(this IServiceCollection services)
    {
        #region Repositories Injection
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
        services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
        services.AddScoped<IBaseRepository<Publisher>, BaseRepository<Publisher>>();
        #endregion

        #region Services Injection
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPublisherService, PublisherService>();
        #endregion


        return services;
    }
}

