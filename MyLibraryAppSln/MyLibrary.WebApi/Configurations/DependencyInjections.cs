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
        services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
        services.AddScoped<IBaseRepository<BookAuthor>, BaseRepository<BookAuthor>>();
        services.AddScoped<IBaseRepository<BookCategory>, BaseRepository<BookCategory>>();
        services.AddScoped<IBaseRepository<Copy>, BaseRepository<Copy>>();
        services.AddScoped<IBaseRepository<Reservation>, BaseRepository<Reservation>>();
        #endregion

        #region Services Injection
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICopyService, CopyService>();
        services.AddScoped<IReservationService, ReservationService>();
        #endregion


        return services;
    }
}

