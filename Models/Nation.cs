using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace African_Football_Legends.Models
{
	public class Nation
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Provide valid Nation name")]
		[MinLength(2, ErrorMessage = "Department name musn't be less than 2 character")]
		[MaxLength(35, ErrorMessage = "Department name musn't be more than 35 character")]
		[DisplayName("Nation Name")]
		public string NationName { get; set; }

		[Required(ErrorMessage = "Provide a valid Ranking")]
		[Range(1, 300, ErrorMessage = "Ranking must be between 1 and 211")]
		[DisplayName("Rank")]
		public int FifaRanking { get; set; }

		[Required(ErrorMessage = "Provide a valid number of titles")]
		[Range(0, 8, ErrorMessage = "Titles must be between 0 and 8")]
		[DisplayName("Titles")]
		public int AfricanCupTitles { get; set; }

		[Required(ErrorMessage = "Provide a valid Foundation Year")]
		[Range(1900, 2021, ErrorMessage = "Foundation Year must be between 1900 and 2021")]
		[DisplayName("Foundation Year")]
		public int FoundationYear { get; set; }

		[Required(ErrorMessage = "Provide valid Description")]
		[MinLength(12, ErrorMessage = "Description musn't be less than 12 character")]
		[DisplayName("Description")]
		public string Description { get; set; }

		[DisplayName("Image")]
		[ValidateNever]
		public string ImageUrl { get; set; }

		// Navigation Property
		[ValidateNever]
		public List<Player> Players { get; set; }
	}
}
