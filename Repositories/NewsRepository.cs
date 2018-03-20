using System.Collections.Generic;
using Dapper;
using F1WM.Model;
using F1WM.Utilities;

namespace F1WM.Repositories
{
	public class NewsRepository : INewsRepository
	{
		private DbContext db;
		private SqlStringBuilder sqlStringBuilder;

		public IEnumerable<NewsSummary> GetLatestNews(int? firstId = null, int? count = Constants.NewsCount)
		{
			if (firstId != null)
			{
				return this.db.Connection.Query<NewsSummary>(
					$@"SELECT {this.sqlStringBuilder.GetNewsSummaryFields("n2")}
					FROM f1_news n1
					JOIN f1_news n2
					ON n1.news_id = @firstId
					WHERE n1.news_date >= n2.news_date AND n2.news_hidden = 0
					ORDER BY n2.news_date DESC
					LIMIT 0, @count",
					new { firstId = firstId, count = count });
			}
			else
			{
				return this.db.Connection.Query<NewsSummary>(
					$@"SELECT {this.sqlStringBuilder.GetNewsSummaryFields()}
					FROM f1_news
					WHERE news_hidden = 0
					ORDER BY news_date DESC
					LIMIT 0, @count",
					new { count = count });
			}
		}

		public NewsDetails GetNewsDetails(int id)
		{
			return this.db.Connection.QuerySingle<NewsDetails>(
				$@"SELECT {this.sqlStringBuilder.GetNewsDetailsFields()}
				FROM f1_news
				WHERE news_id = @id",
				new { id = id });
		}

		public NewsRepository(DbContext db, SqlStringBuilder sqlStringBuilder)
		{
			this.db = db;
			this.sqlStringBuilder = sqlStringBuilder;
		}
	}
}