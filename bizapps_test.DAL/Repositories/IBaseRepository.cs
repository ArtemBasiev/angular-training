namespace bizapps_test.DAL.Repositories
{
    public interface IBaseRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }

        int CreateEntity(T entityToCreate);

        int UpdateEntity(T entityToUpdate);

        int DeleteEntity(T entityToDelete);

        T GetEntityById(int entityId);
    }
}