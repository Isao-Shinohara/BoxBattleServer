using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxBattle
{
	public class EFRepository<T> : IRepository<string, T> where T : IEntity<string>
	{
		protected EFContext db;

		public EFRepository(EFContext db)
		{
			this.db = db;
		}

		public virtual async Task Save()
		{
			await db.SaveChangesAsync();
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
			await Task.WhenAll(entityList.Select(async e => {
				await UpdateAsync(e);
			}));
		}
	}
}