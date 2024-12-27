using DataAccessLayer.Interface;
using DataAccessLayer.Models;
namespace BusinessLogicLayer
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository  studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await studentRepository.GetAsync();
        }


        public async Task AddStudentsAsync(Student student)
        {
            await studentRepository.AddAsync(student);
        }
    }
}
