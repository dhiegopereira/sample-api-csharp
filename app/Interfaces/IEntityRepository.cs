namespace sample_api_csharp.Interfaces
{
    public interface IEntityRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        List<TEntity> ReadAll();
        TEntity ReadOne(long id);
        TEntity Update(long id, TEntity entity);
        TEntity Delete(long id);
    }
}