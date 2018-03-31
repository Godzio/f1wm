using System;
using F1WM.Model;
using F1WM.Services;
using Microsoft.AspNetCore.Mvc;

namespace F1WM.Controllers
{
	[Route("api/[controller]")]
	public class HealthCheckController : Controller
	{
		private IHealthCheckService service;
		private ILoggingService logger;

		[HttpGet]
		public HealthCheck CheckHealth()
		{
			try
			{
				return new HealthCheck() { DatabaseStatus = service.GetDatabaseStatus() };
			}
			catch (Exception ex)
			{
				logger.LogError(ex);
				throw ex;
			}
		}

		public HealthCheckController(IHealthCheckService service, ILoggingService logger)
		{
			this.service = service;
			this.logger = logger;
		}
	}
}