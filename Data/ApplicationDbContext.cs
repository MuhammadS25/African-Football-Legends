using African_Football_Legends.Models;
using Microsoft.EntityFrameworkCore;

namespace African_Football_Legends.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Player> Players { get; set; }
		public DbSet<Nation> Nations { get; set; }
	}
}
