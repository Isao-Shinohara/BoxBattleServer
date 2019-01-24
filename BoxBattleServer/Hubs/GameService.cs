using System;
using System.Collections.Generic;
using System.Linq;
using BoxBattle.Interfaces;
using BoxBattleServer.Datas;
using MagicOnion;
using MagicOnion.Server;

namespace BoxBattleServer.Hubs
{
	public class GameService : ServiceBase<IGameService>, IGameService
	{
		private const string enemyUuid = "enemy";

		IPlayerRepository playerRepository;

		public GameService()
		{
			playerRepository = ServiceLocator.Get<IPlayerRepository>();
		}

		public async UnaryResult<List<PlayerData>> JoinBattle(string uuid)
		{
			Logger.Debug($"CreatePlayerAsync: {uuid}");

			// My player.
			var cRandom = new Random();
			var max = Enum.GetValues(typeof(CharacterType)).Cast<int>().Max() + 1;
			var myPlayerData = new PlayerData() {
				Uuid = uuid,
				CharacterType = (CharacterType)cRandom.Next(max),
			};
			await playerRepository.UpdateAsync(uuid, myPlayerData);

			// Enemy player.
			var enemyPlayerData = await playerRepository.GetAsync(enemyUuid);
			if(enemyPlayerData == null) {
				enemyPlayerData = new PlayerData() {
					Uuid = enemyUuid,
					CharacterType = (CharacterType)cRandom.Next(max),
				};
				await playerRepository.UpdateAsync(enemyUuid, enemyPlayerData);
			}

			// All player.
			var playerList = await playerRepository.GetListAsync(new List<string>() { uuid, enemyUuid });

			return playerList;
		}
	}
}