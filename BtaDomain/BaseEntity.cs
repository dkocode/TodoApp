namespace BtaDomain
{
    public abstract class BaseEntity<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}