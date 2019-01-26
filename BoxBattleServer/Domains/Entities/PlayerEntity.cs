namespace BoxBattle
{
	public class PlayerEntity : BaseEntity<string, PlayerData>, IEntity<PlayerData>
	{
		private PlayerData playerData;

		public PlayerEntity(PlayerData playerData) : base(playerData)
		{
			this.playerData = playerData;
		}

		public override string Id { get { return Uuid; } }
		public string Uuid { get { return playerData.Uuid; } }
		public CharacterType CharacterType { get { return playerData.CharacterType; } }

		public override PlayerData GenarateData()
		{
			return new PlayerData {
				Uuid = Uuid,
				CharacterType = CharacterType,
			};
		}
	}
}
