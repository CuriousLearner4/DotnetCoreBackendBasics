namespace StudentAPI.Handlers.Interface
{
    public interface IHandler
    {
        public Task<DateTime?> Handle(int roll);
    }
}
