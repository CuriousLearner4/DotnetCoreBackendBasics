using SchoolManagementSystem.Model;

namespace SchoolManagementSystem.Repository.Interface
{
    public interface IRepository<T>  where T : class
    {
        public Task<IEnumerable<T>> GetStudents();
        public Task<T> GetStudent(int id);
        public Task CreateStudent(T entity);
        public Task UpdateStudent(T entity);
        public Task DeleteStudent(T entity);
    }
}
