using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models;
using WarhammerCore.Business;
using WarhammerCore.Data;
using WarhammerCore.Data.Models;
using Xunit.Abstractions;

namespace WarhammerCore.Tests.Unit.Tools
{
    public class ServiceFixture
    {
        private IConfigurationRoot _configuration;
        private IServiceCollection _services;
        private SqliteConnection _connection;

        public ServiceFixture()
        {
            _configuration = new ConfigurationBuilder().Build();
        }

        public IServiceCollection Services => _services;

        public IServiceProvider GetScope(ITestOutputHelper output = null)
        {
            ServiceCollection baseCollection = new ServiceCollection();

            string connectionString = "DataSource=file::memory:?cache=private";
            _connection = new SqliteConnection(connectionString);
            _connection.Open();

            baseCollection.AddDbContext<WarhammerDbContext>(o => o.UseSqlite(_connection));
            baseCollection.AddScoped<IProfessionService, ProfessionService>();
            baseCollection.AddScoped<IDataRepo, DataRepo>();
            baseCollection.AddServiceOptions<AppSettings>("AppSettings");

            _services = baseCollection;

            ServiceProvider serviceProvider = baseCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }

    public static class ServicesExtension
    {
        /// <summary>
        /// Go through all the configurations and find the section we are looking for. Assign the values to the model.
        /// </summary>
        /// <typeparam name="TOptions">Model class for the settings section.</typeparam>
        /// <param name="sectionName">Section name, for example in appsettings.json</param>
        /// <returns></returns>
        public static IServiceCollection AddServiceOptions<TOptions>(this IServiceCollection services, string sectionName) where TOptions : class
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