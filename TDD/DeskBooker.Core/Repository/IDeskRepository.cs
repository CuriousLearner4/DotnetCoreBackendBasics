using DeskBooker.Core.Domain;

namespace DeskBooker.Core.Repository
{
    public interface IDeskRepository
    {
        public IEnumerable<Desk> GetAllAvailableDesks(DateTime date);
    }
}
