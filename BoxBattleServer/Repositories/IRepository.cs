namespace BoxBattleServer
{
	public interface IRepository
	{
		T Get<T>(string key);
		void Set<T>(string key, T value);
	}
}