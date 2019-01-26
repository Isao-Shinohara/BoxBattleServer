using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class BattleService : BaseService{

		public async Task<BattleData> InitializeBattle(string uuid)
		{
			// Battle data.
			var battleEntity = await battleRepository.GetAsync(BattleEntity.Key);
			if (battleEntity == null) {
				battleEntity = new BattleEntity(new BattleData());
			}

			// My player.
			var cRandom = new Random();
			var max = Enum.GetValues(typeof(CharacterType)).Cast<int>().Max() + 1;
			var myPlayerData = new PlayerData {
				Uuid = uuid,
				CharacterType = (CharacterType)cRandom.Next(max),
			};
			if (battleEntity.PlayerList.Any(x => x.Uuid == uuid)) {
				battleEntity.PlayerList.RemoveAll(x => x.Uuid == uuid);
			}
			battleEntity.SetMyPlayerData(myPlayerData);
			battleEntity.PlayerList.Add(myPlayerData);
			var playerEntity = new PlayerEntity(myPlayerData);
			await playerRepository.UpdateAsync(playerEntity);

			// Enemy player.
			var enemyPlayerEntity = await playerRepository.GetAsync(PlayerEntity.EnemyUuid);
			if (enemyPlayerEntity == null) {
				var enemyData = new PlayerData {
					Uuid = PlayerEntity.EnemyUuid,
					CharacterType = (CharacterType)cRandom.Next(max),
				};
				enemyPlayerEntity = new PlayerEntity(enemyData);
				await playerRepository.UpdateAsync(enemyPlayerEntity);
				battleEntity.PlayerList.Add(enemyData);
			}
			battleEntity.SetEnemyPlayerData(enemyPlayerEntity.GenarateData());

			// All player.
			await battleRepository.UpdateAsync(battleEntity);
			battleEntity = await battleRepository.GetAsync(BattleEntity.Key);

			return battleEntity.GenarateData();
		}
	}
}