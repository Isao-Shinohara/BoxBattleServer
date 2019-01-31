using Microsoft.EntityFrameworkCore;

namespace BoxBattle
{
	public class EFContext : DbContext
	{
		public EFContext(DbContextOptions<EFContext> options) : base(options)
		{
		}

		public DbSet<PlayerEntity> Players { get; set; }
		public DbSet<BattleEntity> Battles { get; set; }
	}
}
