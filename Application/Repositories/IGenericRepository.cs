using System.Linq.Expressions;

namespace CarShowroomAPI_AI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetPaged(int pageIndex, int pageSize);
        Task<T> GetById(int id);
        Task<int> Count();
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task SaveChanges();
    }
}
