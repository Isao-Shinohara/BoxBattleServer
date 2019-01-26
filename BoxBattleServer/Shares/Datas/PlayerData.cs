using MessagePack;

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
		[Key(0)]
		public string Uuid { get; set; }

		[Key(1)]
		public CharacterType CharacterType { get; set; }
	}
}
