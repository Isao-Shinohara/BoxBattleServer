﻿using System;
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
		private const string battleDataKey = "battleData";
		private const string enemyUuidKey = "enemy";

		IBattleRepository battleRepository;
		IPlayerRepository playerRepository;

		public GameService()
		{
			battleRepository = ServiceLocator.Get<IBattleRepository>();
			playerRepository = ServiceLocator.Get<IPlayerRepository>();
		}

		public async UnaryResult<BattleData> JoinBattle(string uuid)
		{
			Logger.Debug($"CreatePlayerAsync: {uuid}");

			// Battle data.
			var battleData = await battleRepository.GetAsync(battleDataKey);
			if(battleData == null) {
				battleData = new BattleData();
			}

			// My player.
			var cRandom = new Random();
			var max = Enum.GetValues(typeof(CharacterType)).Cast<int>().Max() + 1;
			var myPlayerData = new PlayerData {
				Uuid = uuid,
				CharacterType = (CharacterType)cRandom.Next(max),
			};
			if(battleData.PlayerList.Any(x => x.Uuid == uuid)){
				battleData.PlayerList.RemoveAll(x => x.Uuid == uuid);
			}
			battleData.MyPlayerData = myPlayerData;
			battleData.PlayerList.Add(myPlayerData);
			await playerRepository.UpdateAsync(uuid, myPlayerData);

			// Enemy player.
			var enemyPlayerData = await playerRepository.GetAsync(enemyUuidKey);
			if(enemyPlayerData == null) {
				enemyPlayerData = new PlayerData {
					Uuid = enemyUuidKey,
					CharacterType = (CharacterType)cRandom.Next(max),
				};
				await playerRepository.UpdateAsync(enemyUuidKey, enemyPlayerData);
			}
			battleData.EnemyPlayerData = enemyPlayerData;
			battleData.PlayerList.Add(enemyPlayerData);

			// All player.
			await battleRepository.UpdateAsync(battleDataKey, battleData);
			battleData = await battleRepository.GetAsync(battleDataKey);

			return battleData;
		}
	}
}