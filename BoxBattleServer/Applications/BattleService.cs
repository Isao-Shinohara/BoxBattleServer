using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class BattleService : BaseService{

		public async Task<BattleData> InitializeBattle(string uuid)
		{
			// Battle.
			var battle = await battleRepository.GetAsync(BattleEntity.Key);
			if (battle == null) {
				battle = new BattleEntity();
			}

			// My player.
			var cRandom = new Random();
			var max = Enum.GetValues(typeof(CharacterType)).Cast<int>().Max() + 1;
			var myPlayer = new PlayerEntity(uuid, (CharacterType)cRandom.Next(max));
			battle.SetMyPlayer(myPlayer);
			battle.UpdatePlayer(myPlayer);
			await playerRepository.UpdateAsync(myPlayer);

			// Enemy player.
			var enemyPlayer = await playerRepository.GetAsync(PlayerEntity.EnemyUuid);
			if (enemyPlayer == null) {
				enemyPlayer = new PlayerEntity(PlayerEntity.EnemyUuid, (CharacterType)cRandom.Next(max));
				await playerRepository.UpdateAsync(enemyPlayer);
				battle.UpdatePlayer(enemyPlayer);
			}
			battle.SetEnemyPlayer(enemyPlayer);

			// All player.
			await battleRepository.UpdateAsync(battle);
			battle = await battleRepository.GetAsync(BattleEntity.Key);

			return battle.GenarateData();
		}
	}
}