using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BoxBattle
{
	public class BattleEntity : IEntity
	{
		public static readonly string Key = "battle";

		[DataMember]
		public object Id { get { return Key; } }

		[DataMember]
		public PlayerEntity EnemyPlayer { get; private set; }

		[DataMember]
		private List<PlayerEntity> playerList = new List<PlayerEntity>();

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

		public void LeavePlayer(PlayerEntity player)
		{
			playerList.RemoveAll(x => x.Uuid == player.Uuid);
		}

		public BattleData GenarateData()
		{
			var ss = playerList.Select(x => x.GenarateData()).ToList();

			var data = new BattleData {
				EnemyPlayerData = EnemyPlayer.GenarateData(),
				PlayerList = playerList.Select(x => x.GenarateData()).ToList()
			};
			return data;
		}
	}
}
