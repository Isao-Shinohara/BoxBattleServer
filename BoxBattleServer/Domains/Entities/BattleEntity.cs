using System.Collections.Generic;
using MessagePack;

namespace BoxBattle
{
	[MessagePackObject]
	public class BattleEntity : IEntity
	{
		public static readonly string Key = "battleData";

		public BattleEntity()
		{
		}

		public BattleEntity(BattleData battleData)
		{
			MyPlayerData = battleData.MyPlayerData;
			EnemyPlayerData = battleData.EnemyPlayerData;
			PlayerList = battleData.PlayerList;
		}

		[Key(0)]
		public string Id { get { return Key; } }

		[Key(1)]
		public PlayerData MyPlayerData { get; private set; }

		[Key(2)]
		public PlayerData EnemyPlayerData { get; private set; }

		[Key(3)]
		public List<PlayerData> PlayerList { get; private set; }

		public void SetMyPlayerData(PlayerData playerData)
		{
			MyPlayerData = playerData;
		}

		public void SetEnemyPlayerData(PlayerData playerData)
		{
			EnemyPlayerData = playerData;
		}

		public BattleData GenarateData()
		{
			return new BattleData {
				MyPlayerData = MyPlayerData,
				EnemyPlayerData = EnemyPlayerData,
				PlayerList = PlayerList,
			};
		}
	}
}
