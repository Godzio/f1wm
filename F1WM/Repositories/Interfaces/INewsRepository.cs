using System.Collections.Generic;
using F1WM.ApiModel;

namespace F1WM.Repositories
{
	public interface INewsRepository
	{
		IEnumerable<NewsSummary> GetLatestNews(int count, int? firstId = null);
		NewsDetails GetNewsDetails(int id);
	}
}