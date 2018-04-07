using System.Collections.Generic;
using F1WM.Controllers;
using F1WM.Model;
using F1WM.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace F1WM.UnitTests.Controllers
{
	public class NewsControllerTests
	{
		private NewsController controller;
		private Mock<INewsService> serviceMock;
		private Mock<ICachingService> cacheMock;
		private Mock<ILoggingService> loggerMock;

		public NewsControllerTests()
		{
			serviceMock = new Mock<INewsService>();
			cacheMock = new Mock<ICachingService>();
			loggerMock = new Mock<ILoggingService>();
			cacheMock.Setup(c => c.Get<IEnumerable<NewsSummary>>(It.IsAny<string>())).Returns(() => null);
			cacheMock.Setup(c => c.Get<NewsDetails>(It.IsAny<string>())).Returns(() => null);
			controller = new NewsController(serviceMock.Object, cacheMock.Object, loggerMock.Object);
		}

		[Fact]
		public void ShouldReturnLast20NewsByDefault()
		{
			controller.GetMany(null);

			serviceMock.Verify(s => s.GetLatestNews(20, null), Times.Once);
		}

		[Fact]
		public void ShouldReturnNewsOlderThanFirstId()
		{
			var firstId = 42;

			controller.GetMany(firstId);

			serviceMock.Verify(s => s.GetLatestNews(20, firstId), Times.Once);
		}

		[Fact]
		public void ShouldReturnNewsDetailsById()
		{
			var id = 43;

			controller.GetSingle(id);

			serviceMock.Verify(s => s.GetNewsDetails(id), Times.Once);
		}

		[Fact]
		public void ShouldReturn404IfSingleNewsNotFound()
		{
			var id = 44;

			var result = controller.GetSingle(id);

			serviceMock.Verify(s => s.GetNewsDetails(id), Times.Once);
			Assert.IsType<NotFoundResult>(result);
		}
	}
}