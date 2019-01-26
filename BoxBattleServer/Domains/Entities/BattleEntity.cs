using System.Collections.Generic;

namespace BoxBattle
{
	public class BattleEntity : BaseEntity<string, BattleData>
	{
		public BattleEntity(BattleData battleData) : base(battleData)
		{
			MyPlayerData = battleData.MyPlayerData;
			EnemyPlayerData = battleData.EnemyPlayerData;
			PlayerList = battleData.PlayerList;
		}

		public override string Id { get { return "battleEntity"; } }
		public PlayerData MyPlayerData { get; private set; }
		public PlayerData EnemyPlayerData { get; private set; }
		public List<PlayerData> PlayerList { get; private set; }

		public void SetMyPlayerData(PlayerData playerData)
		{
			MyPlayerData = playerData;
		}

		public void SetEnemyPlayerData(PlayerData playerData)
		{
			EnemyPlayerData = playerData;
		}

		public override BattleData GenarateData()
		{
			return new BattleData {
				MyPlayerData = MyPlayerData,
				EnemyPlayerData = EnemyPlayerData,
				PlayerList = PlayerList,
			};
		}
	}
}
