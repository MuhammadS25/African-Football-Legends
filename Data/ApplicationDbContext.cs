using African_Football_Legends.Models;
using Microsoft.EntityFrameworkCore;

namespace African_Football_Legends.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		DbSet<Player> Players { get; set; }
		DbSet<Nation> Nations { get; set; }
	}
}
