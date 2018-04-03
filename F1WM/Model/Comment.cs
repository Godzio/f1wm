using System;

namespace F1WM.Model
{
	public class Comment
	{
		public int Id { get; set; }
		public int PosterId { get; set; }
		public string PosterName { get; set; }
		public DateTime Date { get; set; }
		public int NewsId { get; set; }
		public string Text { get; set; }
	}
}