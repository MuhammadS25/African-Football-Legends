using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace African_Football_Legends.Models
{
	public class WikipediaLinkAttribute : ValidationAttribute
	{
		private const string WikipediaPattern = @"^https?:\/\/([a-z]+\.)?wikipedia\.org\/wiki\/[^\s]+$";

		public WikipediaLinkAttribute()
		{
			ErrorMessage = "The provided Wikipedia link is not valid.";
		}

		public override bool IsValid(object value)
		{
			if (value == null)
				return false;

			string link = value.ToString().Trim();

			return Regex.IsMatch(link, WikipediaPattern, RegexOptions.IgnoreCase);
		}
	}
}
