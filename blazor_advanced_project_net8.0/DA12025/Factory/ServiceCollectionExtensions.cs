using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;
using Services.Interfaces.Repositories;

namespace Factory
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ISecureDataService, SecureDataService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IMovieService, MovieService>();
            serviceCollection.AddScoped<ISessionService, SessionService>();
        }

        public static void AddDataAccess(IServiceCollection serviceCollection)
        {
            /* InMemory Repositories */
            //serviceCollection.AddSingleton<IUserRepository, InMemoryUserRepository>();
            //serviceCollection.AddSingleton<IMovieRepository, InMemoryMovieRepository>();

            /* Entity Framework Repositories */
            serviceCollection.AddScoped<IMovieRepository, EFMovieRepository>();
            serviceCollection.AddScoped<IUserRepository, EFUserRepository>();
        }
    }
}