namespace StudentAPI.Data.Repository
{
    public interface IStudentRepository
    {
        public Task<DateTime?> GetDOBAsync(int rollno);
    }
}
