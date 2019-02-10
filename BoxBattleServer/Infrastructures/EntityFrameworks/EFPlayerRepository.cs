using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class EFPlayerRepository : EFRepository<PlayerEntity>, IPlayerRepository
	{
		public EFPlayerRepository(EFContext db) : base(db)
		{
		}

		public override async Task<PlayerEntity> GetAsync(string key)
		{
			return db.Players.FirstOrDefault(x => x.Id == key);
		}

		public override async Task<List<PlayerEntity>> GetListAsync(List<string> keyList)
		{
			return db.Players.Where(x => keyList.Contains(x.Id)).ToList();
		}

		public override async Task UpdateAsync(PlayerEntity entity)
		{
			if (!db.Players.Contains(entity)) {
				db.Players.Add(entity);
			}
		}
	}
}
