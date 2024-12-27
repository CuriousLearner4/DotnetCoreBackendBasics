
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Model;

namespace SchoolManagementSystem.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Student> Students { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
