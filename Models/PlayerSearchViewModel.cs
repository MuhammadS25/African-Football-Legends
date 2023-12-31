﻿namespace African_Football_Legends.Models
{
	public class PlayerSearchViewModel
	{
		public string SearchTerm { get; set; }
		public Player.PlayerPosition PositionFilter { get; set; }
		public int NationFilter { get; set; }
		public List<Player> Players { get; set; }

		public bool byGoals { get; set; } = false;
    }
}
