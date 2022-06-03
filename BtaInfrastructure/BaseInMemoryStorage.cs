using System.Runtime.CompilerServices;
using BtaDomain;
using BtaDomainInterfaces;

namespace BtaInfrastructure
{
    public class BaseInMemoryStorage<TEntity, TKey>
        where TEntity : BaseEntity<TKey> where TKey : struct
    {
        private static readonly BaseInMemoryStorage<TEntity, TKey> _instance = new ();
        private readonly List<TEntity> _list;

        static BaseInMemoryStorage()
        { }

        private BaseInMemoryStorage()
        {
            _list = new List<TEntity>(0);
        }

        public static BaseInMemoryStorage<TEntity, TKey> Instance => _instance;

        public IList<TEntity> List => _list;
    }
}