using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class BattleService : BaseService{

		public async Task<BattleEntity> InitializeBattle(string uuid)
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
			var enemyPlayer = await playerRepository.GetAsync(PlayerData.EnemyUuid);
			if (enemyPlayer == null) {
				enemyPlayer = new PlayerEntity(PlayerData.EnemyUuid, (CharacterType)cRandom.Next(max));
				await playerRepository.UpdateAsync(enemyPlayer);
				battle.UpdatePlayer(enemyPlayer);
			}
			battle.SetEnemyPlayer(enemyPlayer);

			// All player.
			await battleRepository.UpdateAsync(battle);
			battle = await battleRepository.GetAsync(BattleEntity.Key);

			return battle;
		}

		public async Task<PlayerEntity> JoinAsync(string uuid)
		{
			return await playerRepository.GetAsync(uuid);
		}

		public async Task<PlayerEntity> LeaveAsync(string uuid)
		{
			return await playerRepository.GetAsync(uuid);
		}

		public async Task<(PlayerEntity, List<PlayerEntity>)> Attack(string attackerUuid, int attackerMp, List<string> defenderUuidList)
		{
			var attacker = await playerRepository.GetAsync(attackerUuid);
			var defenderList = await playerRepository.GetListAsync(defenderUuidList);

			defenderList.ForEach(defender => {
				defender.Damage(attackerMp);
			});

			await playerRepository.UpdateListAsync(defenderList);

			return (attacker, defenderList);
		}
	}
}