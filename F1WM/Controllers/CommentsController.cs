using System;
using System.Collections.Generic;
using F1WM.Model;
using F1WM.Services;
using Microsoft.AspNetCore.Mvc;

namespace F1WM.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : Controller
	{
		private ICommentsService service;
		private ILoggingService logger;

		[HttpGet]
		public IEnumerable<Comment> GetMany([FromQuery(Name = "newsId")] int newsId)
		{
			try
			{
				return service.GetCommentsByNewsId(newsId);
			}
			catch (Exception ex)
			{
				logger.LogError(ex);
				throw ex;
			}
		}

		[HttpGet("{id}")]
		public Comment GetSingle(int id)
		{
			try
			{
				return service.GetComment(id);
			}
			catch (Exception ex)
			{
				logger.LogError(ex);
				throw ex;
			}

		}

		public CommentsController(ICommentsService service, ILoggingService logger)
		{
			this.service = service;
			this.logger = logger;
		}
	}
}