using System.IO;

namespace F1WM.Utilities
{
	public static class StringExtensions
	{
		public static string ParseImageInformation(this string text)
		{
			using (var reader = new StringReader(text))
			{
				if (reader.Peek() == '@')
				{
					var firstLine = reader.ReadLine();
					var imageSource = firstLine.Split(',')[2];
					imageSource = imageSource.StartsWith("http") ? imageSource : $"/img/news/{imageSource}";
					var imageInformation = $"<img src=\"{imageSource}\">";
					text.Replace(firstLine, imageInformation);
				}
				return text;
			}
		}
	}
}