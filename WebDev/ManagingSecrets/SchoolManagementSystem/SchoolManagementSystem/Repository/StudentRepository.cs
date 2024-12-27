using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Model;
using SchoolManagementSystem.Repository.Interface;

namespace SchoolManagementSystem.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        private ApplicationDbContext _db { get; init; }

        public async Task CreateStudent(Student student)
        {
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s=>s.Id==id);
            return student;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var students = await _db.Students.ToListAsync();
            return students;
        }

        public async Task UpdateStudent(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
        }
    }
}
