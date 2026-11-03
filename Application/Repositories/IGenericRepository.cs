namespace CarShowroomAPI_AI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task SaveChanges();
    }
}
