using DataAccessLayer.Data;
using DataAccessLayer.Models;
namespace DataAccessLayer.Repository
{
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
