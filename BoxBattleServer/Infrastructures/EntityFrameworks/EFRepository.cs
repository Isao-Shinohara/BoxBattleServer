using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class EFRepository<T> : IRepository<T> where T : IEntity
	{
		protected EFContext db;

		public EFRepository(EFContext db)
		{
			this.db = db;
		}

		public virtual async Task<T> GetAsync(string key)
		{
			return default(T);
		}

		public virtual async Task<List<T>> GetListAsync(List<string> keyList)
		{
			return new List<T>();
		}

		public virtual async Task UpdateAsync(T entity)
		{
		}

		public virtual async Task UpdateListAsync(List<T> entityList)
		{
		}
	}
}