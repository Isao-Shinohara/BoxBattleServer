using MessagePack;
using UnityEngine;

public enum CharacterType
{
	Badger,
	Deer,
	Dog,
	Lion,
	Lizard,
	Owl,
	Rabbit,
	Rat,
}

namespace BoxBattle
{
	[MessagePackObject]
	public class PlayerData
	{
		[IgnoreMember]
		public const string EnemyUuid = "enemy";

		[IgnoreMember]
		public const int ConstMaxHp = 20000;

		[IgnoreMember]
		public const int ConstMaxMp = 100;

		[Key(0)]
		public string Uuid { get; set; }

		[Key(1)]
		public CharacterType CharacterType { get; set; }

		[Key(2)]
		public int Hp { get; set; }

		[Key(3)]
		public int MaxHp { get; set; }

		[Key(4)]
		public int Mp { get; set; }

		[Key(5)]
		public int MaxMp { get; set; }

		[Key(6)]
		public bool IsDied { get; set; }

		[Key(7)]
		public int DamagePoint { get; set; }

		[Key(8)]
		public Vector3 Position { get; set; }

		[Key(9)]
		public Quaternion Rotation { get; set; }

		public PlayerData()
		{
		}

		public PlayerData(string uuid, CharacterType characterType)
		{
			Uuid = uuid;
			CharacterType = characterType;
			Hp = MaxHp = ConstMaxHp;
			MaxMp = ConstMaxMp;
		}
	}
}
