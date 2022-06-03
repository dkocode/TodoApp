namespace BtaApplication.Common
{
    public interface IValidationFactory<in TEntity> where TEntity : class
    {
        ValidationResult Validate(TEntity entity);
    }
}