using FluentValidation.AspNetCore;
using HostApp.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using Microsoft.IdentityModel.Tokens;
=======
using WarhammerCore.WebApi.Middleware.Websocket;
>>>>>>> signalr
using System.IO;
using System.Text;
using WarhammerCore.Data.Models;
using WarhammerCore.WebApi.Extensions;
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
			services.AddCors(options =>
					options.AddPolicy("AllowAll", builder =>
							builder
									.AllowCredentials()
									.SetIsOriginAllowed(origin => true)
									.AllowAnyMethod()
									.AllowAnyHeader()
					));
			services.AddSignalR();
			services.AddControllers();
			services.AddMvc(options => options.Filters.Add<ValidationFilter>(int.MinValue)).AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
			services.AddDbContext<WarhammerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
			services.AddSwaggerGen();
			services.AddAppServices();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = Configuration["Jwt:Issuer"],
							ValidAudience = Configuration["Jwt:Issuer"],
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
						};
					});
			services.AddSingleton<IConfiguration>(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			string path = Directory.GetCurrentDirectory();
			loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warhammer")).UseCors("AllowAll");

			/* app.UseMvc(routes =>
			 {
					 routes.MapRoute(name: "Chat", template: "Chat/Join");
			 });
*/
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();


			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<ChatHub>("/chat");
				endpoints.MapControllers();
			});

			app.UseErrorHandling();


		}
	}
}