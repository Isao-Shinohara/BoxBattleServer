using System.Collections.Generic;
using MessagePack;

namespace BoxBattleServer.Datas
{
	[MessagePackObject]
	public class BattleData
	{
		[Key(0)]
		public List<PlayerData> PlayerList { get; set; }
	}
}
