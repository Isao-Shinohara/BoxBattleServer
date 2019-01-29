using System.Collections.Generic;
using MessagePack;

namespace BoxBattle
{
	[MessagePackObject]
	public class BattleData
	{
		[Key(0)]
		public PlayerData EnemyPlayerData;

		[Key(1)]
		public List<PlayerData> PlayerList { get; set; } = new List<PlayerData>();
	}
}
