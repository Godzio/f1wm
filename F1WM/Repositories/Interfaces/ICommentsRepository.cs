using System.Collections.Generic;
using System.Threading.Tasks;
using F1WM.ApiModel;

namespace F1WM.Repositories
{
	public interface ICommentsRepository
	{
		Task<IEnumerable<Comment>> GetCommentsByNewsId(int newsId);
		Task<Comment> GetComment(int id);
	}
}