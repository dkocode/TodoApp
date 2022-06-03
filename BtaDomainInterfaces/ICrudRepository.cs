using BtaDomain;

namespace BtaDomainInterfaces
{
    public interface ICrudRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey> 
        where TKey : struct
    {
        void Insert(TEntity entity);
        
        IList<TEntity> GetAll();

        bool Update(TEntity item);

        bool Remove(TEntity item);
        
        TEntity? GetById(TKey id);
    }
}