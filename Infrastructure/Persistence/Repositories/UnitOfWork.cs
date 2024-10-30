using System.Collections.Concurrent;

namespace Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _storeContexct;
		private readonly ConcurrentDictionary<string, object> _repositories;
		public UnitOfWork(StoreContext storeContexct)
		{
			_storeContexct = storeContexct;
			_repositories = new ConcurrentDictionary<string, object>();
		}

		 
		public async Task<int> SaveChangesAsync()=> await _storeContexct.SaveChangesAsync();

		 
		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		 => (GenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(_storeContexct));


	}
}
