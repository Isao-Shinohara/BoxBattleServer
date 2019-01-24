using System.Collections.Generic;
using MessagePack;

namespace BoxBattleServer.Datas
{
	[MessagePackObject]
	public class BattleData
	{
		[Key(0)]
		public PlayerData MyPlayerData;

		[Key(1)]
		public PlayerData EnemyPlayerData;

		[Key(2)]
		public List<PlayerData> PlayerList { get; set; } = new List<PlayerData>();
	}
}
