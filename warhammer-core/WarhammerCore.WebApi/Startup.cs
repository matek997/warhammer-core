using HostApp.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarhammerCore.Data.Models;
using WarhammerCore.WebApi.Extensions;
using FluentValidation.AspNetCore;
using WarhammerCore.WebApi.Validation;

namespace WarhammerCore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(options => options.Filters.Add<ValidationFilter>(int.MinValue)).AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddControllersWithViews();
            services.AddDbContext<WarhammerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            services.AddSwaggerGen().AddControllers();

            services.AddAppServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseErrorHandling();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warhammer"));
        }
    }
}
