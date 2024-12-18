using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;
using Infrastructure.Interfaces;
using Core.Interfaces;
using Core.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IStorageService, StorageService>();
            
            // Register Repositories (If necessary)
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
