namespace BoxBattle
{
	public class BaseEntity<T, U> : IEntity<U> where U : BaseData
	{
		public BaseEntity(BaseData data) {}
		public virtual T Id { get; }

		public virtual U GenarateData()
		{
			throw new System.NotImplementedException();
		}
	}
}
