using DeskBooker.Core.Domain;
using DeskBooker.Core.Repository;
namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository _deskBookingRepository;
        private readonly IDeskRepository _deskRepository;

        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
        {
            this._deskBookingRepository = deskBookingRepository;
            this._deskRepository = deskRepository;
        }

        public DeskBookingResponse BookDesk(DeskBookingRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            DeskBookingResponse response = DeskBookingRequestProcessorHelpers.Map<DeskBookingResponse>(request);
            if (_deskRepository.GetAllAvailableDesks(request.Date).FirstOrDefault() is Desk availableDesk){
                DeskBooking deskBooking = DeskBookingRequestProcessorHelpers.Map<DeskBooking>(request);
                deskBooking.DeskId = availableDesk.Id;
                _deskBookingRepository.SaveBooking(deskBooking);
                response.DeskBookingId = deskBooking.Id;
                response.Code = DeskBookingResultCode.Success;
            }
            else
            {
                response.Code = DeskBookingResultCode.NoDeskAvailable;
            }
            
            return response;
        }
    }

}
