﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace African_Football_Legends.Models
{
	public class Player
	{
		public enum PlayerPosition
		{
			LM,   // Left Midfielder
			RM,   // Right Midfielder
			GK,   // Goalkeeper
			LW,   // Left Winger
			LWB,  // Left Wing-Back
			RWB,  // Right Wing-Back
			LB,   // Left Back
			RB,   // Right Back
			CB,   // Center Back
			CDM,  // Central Defensive Midfielder
			CM,   // Central Midfielder
			CAM,  // Central Attacking Midfielder
			CF,   // Center Forward
			ST,   // Striker
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Provide valid Full name")]
		[MinLength(3, ErrorMessage = "Full name musn't be less than 3 character")]
		[MaxLength(70, ErrorMessage = "Full name musn't be more than 70 character")]
		[DisplayName("Name")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Provide valid Summary")]
		[MinLength(12, ErrorMessage = "Summary musn't be less than 12 character")]
		[DisplayName("Career Summary")]
		public string Summary { get; set; }

		[Display(Name = "Date of Birth")]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Provide valid Position")]
		[DisplayName("Position")]
		public PlayerPosition Position { get; set; }

		[Required(ErrorMessage = "Provide valid number of caps")]
		[Range(1, 200, ErrorMessage = "Caps must be between 1 and 200")]
		public short International_Caps { get; set; }

		[DisplayName("Image")]
		[ValidateNever]
		public string ImageUrl { get; set; }

		// Foreign Key.
		[DisplayName("Department")]
		[Range(1, int.MaxValue, ErrorMessage = "Select Valid Department")]
		public int NationId { get; set; }

		// Navigation Property.
		[ValidateNever]
		public Nation Nation { get; set; }
	}
}
