using DeskBooker.Core.Domain;
namespace DeskBooker.Core.Processor
{
    public static class DeskBookingRequestProcessorHelpers
    {

        public static T Map<T>(DeskBookingRequest request) where T : DeskBookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}
