using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace BoxBattle
{
	[MessagePackObject]
	public class BattleEntity : IEntity
	{
		public static readonly string Key = "battle";

		[Key(0)]
		public object Id { get { return Key; } }

		[Key(1)]
		public PlayerEntity MyPlayer { get; private set; }

		[Key(2)]
		public PlayerEntity EnemyPlayer { get; private set; }

		[Key(3)]
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
