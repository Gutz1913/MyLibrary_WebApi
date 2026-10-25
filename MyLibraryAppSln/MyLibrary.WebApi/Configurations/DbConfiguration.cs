using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.Models;

namespace MyLibrary.WebApi.Configurations;

public static class DbConfiguration
{
    public static IServiceCollection SetDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = Environment.GetEnvironmentVariable(MyLibraryLiterals.CONNECTION_STRING);
        //var connectionString = "Server=localhost,14330;Database=MyLibraryAppDb;User Id=sa;Password=Abc.123456;TrustServerCertificate=true;";
        services.AddDbContext<MyLibraryAppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                )
            );
        return services;
    }
}
