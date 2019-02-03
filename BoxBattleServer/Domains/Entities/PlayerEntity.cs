using System.Runtime.Serialization;
using UnityEngine;

namespace BoxBattle
{
	public class PlayerEntity : IEntity<string>
	{
		public PlayerEntity()
		{
		}

		public PlayerEntity(string uuid, CharacterType characterType)
		{
			Id = uuid;
			Uuid = uuid;
			CharacterType = characterType;
			Hp = MaxHp = PlayerData.ConstMaxHp;
			MaxMp = PlayerData.ConstMaxMp;
		}

		[DataMember]
		public string Id { get; private set; }

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

		[DataMember]
		public bool IsDied { get; set; }

		[DataMember]
		public int DamagePoint { get; set; }

		[DataMember]
		public Vector3 Position { get; set; }

		[DataMember]
		public Quaternion Rotation { get; set; }

		public void Damage(int attackerMp)
		{
			DamagePoint = BattleCalculate.Attack(attackerMp);
			IsDied = Hp > 0 && DamagePoint >= Hp;
			Hp = Hp - DamagePoint;
			Hp = Hp > 0 ? Hp : 0;
		}

		public void Recover()
		{
			Hp = MaxHp;
		}

		public void Move(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
		}

		public PlayerData GenarateData()
		{
			var data =  new PlayerData {
				Uuid = Uuid,
				CharacterType = CharacterType,
				Hp = Hp,
				MaxHp = MaxHp,
				Mp = Mp,
				MaxMp = MaxMp,
				IsDied = IsDied,
				DamagePoint = DamagePoint,
				Position = Position,
				Rotation = Rotation,
			};
			return data;
		}
	}
}
