using Sênior.Business.Interfaces;
using Sênior.Business.Notifications;
using Sênior.Business.Services;
using Sênior.Dados.Context;
using Sênior.Dados.Repository;

namespace SêniorTeste.API.Config
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<DbConnectionFactory>();
        }
    }
}
