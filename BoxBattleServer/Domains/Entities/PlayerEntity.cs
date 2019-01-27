using System.Runtime.Serialization;

namespace BoxBattle
{
	public class PlayerEntity : IEntity
	{
		public PlayerEntity()
		{
		}

		public PlayerEntity(string uuid, CharacterType characterType)
		{
			Uuid = uuid;
			CharacterType = characterType;
			Hp = MaxHp = PlayerData.ConstMaxHp;
			MaxMp = PlayerData.ConstMaxMp;
		}

		[DataMember]
		public object Id { get { return Uuid; } }

		[DataMember]
		public string Uuid { get; private set; }

		[DataMember]
		public CharacterType CharacterType { get; private set; }

		[DataMember]
		public int Hp { get; set; }

		[DataMember]
		public int MaxHp { get; set; }

		[DataMember]
		public int Mp { get; set; }

		[DataMember]
		public int MaxMp { get; set; }

		public PlayerData GenarateData()
		{
			var data =  new PlayerData {
				Uuid = Uuid,
				CharacterType = CharacterType,
				Hp = Hp,
				MaxHp = MaxHp,
				Mp = Mp,
				MaxMp = MaxMp,
			};
			return data;
		}
	}
}
