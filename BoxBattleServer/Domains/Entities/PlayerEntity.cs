using System.Runtime.Serialization;

namespace BoxBattle
{
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

		[DataMember(Order = 0)]
		public object Id { get { return Uuid; } }

		[DataMember(Order = 1)]
		public string Uuid { get; private set; }

		[DataMember(Order = 2)]
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
