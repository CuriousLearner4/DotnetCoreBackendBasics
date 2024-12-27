using DeskBooker.Core.Domain;
namespace DeskBooker.Core.Repository
{
    public interface IDeskBookingRepository
    {
        public Task SaveBooking(DeskBooking deskBooking);
        
    }
}
