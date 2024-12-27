
using Microsoft.EntityFrameworkCore;

namespace StudentAPI.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<DateTime?> GetDOBAsync(int rollno)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s => s.RollNo == rollno);
            if(student==null) return null;
            return student.DateOfBirth;
        }
    }
}
