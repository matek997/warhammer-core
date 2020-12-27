using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Business;
using WarhammerCore.Data;

namespace WarhammerCore.WebApi.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProfessionService, ProfessionService>();
            serviceCollection.AddScoped<IDataRepo, DataRepo>();

            return serviceCollection;
        }
    }
}
