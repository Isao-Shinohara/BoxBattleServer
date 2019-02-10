
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class EFBattleRepository : EFRepository<BattleEntity>, IBattleRepository
	{
		public EFBattleRepository(EFContext db) : base(db)
		{
		}

		public override async Task<BattleEntity> GetAsync(string key)
		{
			return db.Battles.FirstOrDefault(x => x.Id == key);
		}

		public override async Task<List<BattleEntity>> GetListAsync(List<string> keyList)
		{
			return db.Battles.Where(x => keyList.Contains(x.Id)).ToList();
		}

		public override async Task UpdateAsync(BattleEntity entity)
		{
			if (!db.Battles.Contains(entity)) {
				db.Battles.Add(entity);
			}
		}
	}
}
