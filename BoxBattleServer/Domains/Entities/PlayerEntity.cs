using MessagePack;

namespace BoxBattle
{
	[MessagePackObject]
	public class PlayerEntity : IEntity
	{
		public const string EnemyUuid = "enemy";

		public PlayerEntity()
		{
		}

		public PlayerEntity(PlayerData playerData)
		{
			Uuid = playerData.Uuid;
			CharacterType = playerData.CharacterType;
		}

		[Key(0)]
		public string Id { get { return Uuid; } }

		[Key(1)]
		public string Uuid { get; set; }

		[Key(2)]
		public CharacterType CharacterType { get; set; }

		public PlayerData GenarateData()
		{
			return new PlayerData {
				Uuid = Uuid,
				CharacterType = CharacterType,
			};
		}
	}
}
