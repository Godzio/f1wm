﻿using System;
using System.Data;
using System.Reflection;
using F1WM.DatabaseModel;
using F1WM.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.WebApi;

namespace F1WM
{
	public class Startup
	{
		private const string corsPolicy = "DefaultPolicy";
		private LoggingService logger;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			logger = new LoggingService(configuration);
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			try
			{
				services
					.AddMvcCore()
					.AddApiExplorer()
					.AddAuthorization()
					.AddDataAnnotations()
					.AddFormatterMappings()
					.AddCors(o => o.AddPolicy(corsPolicy, GetCorsPolicyBuilder()))
					.AddJsonFormatters();

				services
					.AddLogging()
					.AddTransient<ILoggingService, LoggingService>(provider => this.logger)
					.AddMemoryCache()
					.ConfigureRepositories(Configuration)
					.ConfigureLogicServices();
			}
			catch (Exception ex)
			{
				logger.LogError(ex);
				throw ex;
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder application,
			IHostingEnvironment environment,
			IServiceProvider serviceProvider,
			IConfigurationBuilder configurationBuilder)
		{
			try
			{
				if (environment.IsDevelopment())
				{
					application.UseDeveloperExceptionPage();
					configurationBuilder.AddUserSecrets<Startup>();
				}
				configurationBuilder.AddEnvironmentVariables();

				application
					.UseForwardedHeaders(GetForwardedHeadersOptions())
					.UseCors(corsPolicy)
					.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, GetSwaggerUiSettings())
					.UseMvc();

				SetDbEncoding(serviceProvider);
			}
			catch (Exception ex)
			{
				logger.LogError(ex);
				throw ex;
			}
		}

		private static void SetDbEncoding(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetService<F1WMContext>();
			context.Database.OpenConnection();
			using (var connection = context.Database.GetDbConnection())
			{
				var command = connection.CreateCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = "SET NAMES utf8mb4; ";
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		private Action<CorsPolicyBuilder> GetCorsPolicyBuilder()
		{
			return builder =>
			{
				builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials();
			};
		}

		private ForwardedHeadersOptions GetForwardedHeadersOptions()
		{
			return new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			};
		}

		private Action<SwaggerUiSettings<WebApiToSwaggerGeneratorSettings>> GetSwaggerUiSettings()
		{
			return settings =>
			{
				settings.GeneratorSettings.Title = "F1WM web API";
				settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
			};
		}
	}
}