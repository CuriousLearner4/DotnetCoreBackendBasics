using DataAccessLayer.Models;
namespace BusinessLogicLayer
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task AddStudentsAsync(Student student);
    }
}
