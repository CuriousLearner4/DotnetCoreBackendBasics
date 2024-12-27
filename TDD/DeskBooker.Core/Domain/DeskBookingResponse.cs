using DeskBooker.Core.Processor;

namespace DeskBooker.Core.Domain
{
    public class DeskBookingResponse : DeskBookingBase
    {
        public int? DeskBookingId { get;set; }
        public DeskBookingResultCode Code { get; set; }
    }
}
