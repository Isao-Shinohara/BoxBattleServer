﻿using MagicOnion;
using MagicOnion.Server;

namespace BoxBattle
{
	public class GameRpc : ServiceBase<IGameRpc>, IGameRpc
	{
		BattleService battleService;

		public GameRpc()
		{
			battleService = ServiceLocator.Get<BattleService>();
		}

		public async UnaryResult<BattleData> InitializeBattle(string uuid)
		{
			var battleEntiry = await battleService.InitializeBattle(uuid);
			return battleEntiry.GenarateData();
		}
	}
}