using System.Collections.Generic;
using MessagePack;

namespace BoxBattle
{
	[MessagePackObject]
	public class AttackResultData
	{
		[Key(0)]
		public PlayerData AttackerPlayerData;

		[Key(1)]
		public List<PlayerData> DefenderPlayerList { get; set; } = new List<PlayerData>();
	}
}
