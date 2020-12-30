using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models;
using WarhammerCore.Business;
using WarhammerCore.Data;

namespace WarhammerCore.WebApi.Extensions
{
    public static class AppServicesExtension
    {
        /// <summary>
        /// Include services with Dependency Injection.
        /// </summary>
        public static IServiceCollection AddAppServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProfessionService, ProfessionService>();
            serviceCollection.AddScoped<IDataRepo, DataRepo>();

            serviceCollection.AddServiceOptions<AppSettings>("AppSettings");

            return serviceCollection;
        }

        /// <summary>
        /// Go through all the configurations and find the section we are looking for. Assign the values to the model.
        /// </summary>
        /// <typeparam name="TOptions">Model class for the settings section.</typeparam>
        /// <param name="sectionName">Section name, for example in appsettings.json</param>
        /// <returns></returns>
        private static IServiceCollection AddServiceOptions<TOptions>(this IServiceCollection services, string sectionName) where TOptions : class
        {
            return services.AddSingleton(sp =>
            {
                IEnumerable<IConfiguration> configurations = sp.GetRequiredService<IEnumerable<IConfiguration>>();
                foreach (var configuration in configurations)
                {
                    var section = configuration.GetSection(sectionName);
                    if (!section.Exists()) continue;

                    return section.Get<TOptions>();
                }

                return default;
            });
        }
    }
}