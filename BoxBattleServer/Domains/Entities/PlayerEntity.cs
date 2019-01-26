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

		public PlayerEntity(string uuid, CharacterType characterType)
		{
			Uuid = uuid;
			CharacterType = characterType;
		}

		[Key(0)]
		public object Id { get { return Uuid; } }

		[Key(1)]
		public string Uuid { get; private set; }

		[Key(2)]
		public CharacterType CharacterType { get; private set; }

		public PlayerData GenarateData()
		{
			var data =  new PlayerData {
				Uuid = Uuid,
				CharacterType = CharacterType,
			};
			return data;
		}
	}
}
