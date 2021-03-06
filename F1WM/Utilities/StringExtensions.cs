using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using F1WM.ApiModel;

namespace F1WM.Utilities
{
	public static class StringExtensions
	{
		public static string ParseImageInformation(this string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				var imageSource = text.Split(',')[2];
				imageSource = imageSource.StartsWith("http") ? imageSource : $"/img/news/{imageSource}";
				var imageInformation = $"<img src=\"{imageSource}\">";
				text = imageInformation;
			}
			return text;
		}

		public static ImportantNewsSummary ParseImportantNews(this string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				var tokens = text.Split('|');
				int id;
				if (tokens.Length != 3 || !int.TryParse(tokens[0], out id)) 
				{
					throw new ArgumentException("Attempted to parse text in an unknown format (it's expected to be: newsID|imageUrl|newsShortText)", nameof(text));
				}
				return new ImportantNewsSummary()
				{
					Id = id,
					ImageUrl = tokens[1].GetImageUrl(),
					ShortText = tokens[2]
				};
			}
			return null;
		}

		public static string GetImageUrl(this string text)
		{
			return $"/img/news/{text}";
		}

		public static string Cleanup(this string text)
		{
			return text?.Replace("[urlb=", "[url=").Replace("[urln=", "[url=");
		}

		public static string GetFlagIconPath(this string id)
		{
			return $"/img/flagi/{id}2.gif";
		}

		public static string GetTrackIconPath(this string id)
		{
			return $"/img/tory/{id}_m2.png";
		}

		public static string GetGrandPrixName(this string genitive)
		{
			return $"Grand Prix {genitive}";
		}
	}
}
