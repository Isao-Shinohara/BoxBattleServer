using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class BattleService : BaseService
	{
		private const string battleDataKey = "battleData";
		private const string enemyUuidKey = "enemy";

		public async Task<BattleData> InitializeBattle(string uuid)
		{
			// Battle data.
			var battleEntity = await battleRepository.GetAsync(battleDataKey);
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
			await playerRepository.UpdateAsync(playerEntity.Id, playerEntity);

			// Enemy player.
			var enemyPlayerEntity = await playerRepository.GetAsync(enemyUuidKey);
			if (enemyPlayerEntity == null) {
				var enemyData = new PlayerData {
					Uuid = enemyUuidKey,
					CharacterType = (CharacterType)cRandom.Next(max),
				};
				enemyPlayerEntity = new PlayerEntity(enemyData);
				await playerRepository.UpdateAsync(enemyPlayerEntity.Id, enemyPlayerEntity);
				battleEntity.PlayerList.Add(enemyData);
			}
			battleEntity.SetEnemyPlayerData(enemyPlayerEntity.GenarateData());

			// All player.
			await battleRepository.UpdateAsync(battleDataKey, battleEntity);
			battleEntity = await battleRepository.GetAsync(battleDataKey);

			var test = battleEntity.GenarateData();

			return battleEntity.GenarateData();
		}
	}
}