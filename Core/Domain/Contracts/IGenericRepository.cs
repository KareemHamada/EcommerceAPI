using Domain.Entities;

namespace Domain.Contracts
{
	public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
	{

		Task<TEntity?> GetAsync(TKey id);
		Task<ICollection<TEntity>> GetAllAsync(bool trackChanges);
		Task AddAsync(TEntity entity);
		void Delete(TEntity entity);
		void Update(TEntity entity);
	}
}
