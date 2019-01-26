using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BoxBattle
{
	public class BattleEntity : IEntity
	{
		public static readonly string Key = "battle";

		[DataMember(Order = 0)]
		public object Id { get { return Key; } }

		[DataMember(Order = 1)]
		public PlayerEntity MyPlayer { get; private set; }

		[DataMember(Order = 2)]
		public PlayerEntity EnemyPlayer { get; private set; }

		[DataMember(Order = 3)]
		private List<PlayerEntity> playerList = new List<PlayerEntity>();

		public void SetMyPlayer(PlayerEntity player)
		{
			MyPlayer = player;
		}

		public void SetEnemyPlayer(PlayerEntity player)
		{
			EnemyPlayer = player;
		}

		public void UpdatePlayer(PlayerEntity player)
		{
			if (playerList.Any(x => x.Uuid == player.Uuid)) {
				playerList.RemoveAll(x => x.Uuid == player.Uuid);
			}
			playerList.Add(player);
		}

		public BattleData GenarateData()
		{
			var ss = playerList.Select(x => x.GenarateData()).ToList();

			var data = new BattleData {
				MyPlayerData = MyPlayer.GenarateData(),
				EnemyPlayerData = EnemyPlayer.GenarateData(),
				PlayerList = playerList.Select(x => x.GenarateData()).ToList()
			};
			return data;
		}
	}
}
