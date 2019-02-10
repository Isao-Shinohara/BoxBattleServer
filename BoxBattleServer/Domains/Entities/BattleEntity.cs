using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BoxBattle
{
	public class BattleEntity : IEntity<string>
	{
		public static readonly string Key = "battle";

		[DataMember]
		public string Id { get; private set; }

		[DataMember]
		public PlayerEntity EnemyPlayer { get; private set; }

		[DataMember]
		private List<PlayerEntity> playerList = new List<PlayerEntity>();

		public void SetEnemyPlayer(PlayerEntity player)
		{
			Id = Key;
			EnemyPlayer = player;
		}

		public void UpdatePlayer(PlayerEntity player)
		{
			LeavePlayer(player);
			playerList.Add(player);
		}

		public void LeavePlayer(PlayerEntity player)
		{
			playerList.RemoveAll(x => x.Uuid == player.Uuid);
		}

		public BattleData GenarateData()
		{
			var data = new BattleData {
				EnemyPlayerData = EnemyPlayer.GenarateData(),
				PlayerList = playerList.Select(x => x.GenarateData()).ToList()
			};
			return data;
		}
	}
}
