namespace BoxBattle
{
	public class BaseService
	{
		protected IBattleRepository battleRepository;
		protected IPlayerRepository playerRepository;

		public BaseService()
		{
			battleRepository = ServiceLocator.Get<IBattleRepository>();
			playerRepository = ServiceLocator.Get<IPlayerRepository>();
		}
	}
}
