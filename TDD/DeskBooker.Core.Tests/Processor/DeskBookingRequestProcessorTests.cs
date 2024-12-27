using DeskBooker.Core.Domain;
using DeskBooker.Core.Repository;
using Moq;
using NUnit.Framework;
namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        private DeskBookingRequestProcessor _processor;
        private DeskBookingRequest _request;
        private List<Desk> _availableDesks;
        private Mock<IDeskBookingRepository> _deskBookingMockRepository;
        private Mock<IDeskRepository> _deskMockRepository;

        [SetUp]
        public void Setup()
        {
            _request = new DeskBookingRequest { FirstName = "Harsha",
                LastName = "Gopisetti", 
                Email = "harshagopisetti@gmail.com", 
                Date = new DateTime(2002, 10, 28) };
            _availableDesks = new List<Desk> { new Desk { Id = 7 } };
            _deskBookingMockRepository = new Mock<IDeskBookingRepository>();
            _deskMockRepository = new Mock<IDeskRepository>();
            _deskMockRepository.Setup(x => x.GetAllAvailableDesks(_request.Date)).Returns(_availableDesks);
            _processor = new DeskBookingRequestProcessor(_deskBookingMockRepository.Object,_deskMockRepository.Object);
        }

        [Test]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            //Arrange
            
           
            //Act
            DeskBookingResponse response = _processor.BookDesk(_request);
            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.FirstName, Is.EqualTo(_request.FirstName));
            Assert.That(response.LastName, Is.EqualTo(_request.LastName));
            Assert.That(response.Email, Is.EqualTo(_request.Email));
            Assert.That(response.Date, Is.EqualTo(_request.Date));

        }

        [Test]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            //Arrange
            //Act
            //DeskBookingResponse response = processor.BookDesk(null);
            //Assert
            var exception =  Assert.Throws<ArgumentNullException>(()=>_processor.BookDesk(null));
            Assert.That( exception.ParamName,Is.EqualTo("request"));

        }
        [Test]
        public void ShouldSaveDeskBooking()
        {
            DeskBooking savedDeskBooking = null;
            _deskBookingMockRepository.Setup(x => x.SaveBooking(It.IsAny<DeskBooking>())).Callback<DeskBooking>(deskBooking => { savedDeskBooking = deskBooking; });
            _processor.BookDesk(_request);
            _deskBookingMockRepository.Verify(x => x.SaveBooking(It.IsAny<DeskBooking>()), Times.Once);
            Assert.NotNull(savedDeskBooking);
            Assert.That(savedDeskBooking.FirstName, Is.EqualTo(_request.FirstName));
            Assert.That(savedDeskBooking.LastName, Is.EqualTo(_request.LastName));
            Assert.That(savedDeskBooking.Email, Is.EqualTo(_request.Email));
            Assert.That(savedDeskBooking.Date, Is.EqualTo(_request.Date));
            Assert.That(_availableDesks.FirstOrDefault().Id, Is.EqualTo(savedDeskBooking.DeskId));
        }
        [Test]
        public void ShouldNotSaveDeskBookindIfNoDeskIsAvailable()
        {
            _availableDesks.Clear();
            _processor.BookDesk(_request);
            _deskBookingMockRepository.Verify(x => x.SaveBooking(It.IsAny<DeskBooking>()), Times.Never);
        }
        [Test]
        [TestCase(DeskBookingResultCode.Success,true)] 
        [TestCase(DeskBookingResultCode.NoDeskAvailable,false)]
        public void ShouldReturnExpectedResultCode(DeskBookingResultCode expectedResultCode,bool isDeskAvailable) 
        {
            if (!isDeskAvailable)
            {
                _availableDesks.Clear();
            }
            var response = _processor.BookDesk(_request);
            Assert.That(expectedResultCode, Is.EqualTo(response.Code));
        }
        [Test]
        [TestCase(1,true)] 
        [TestCase(null,false)]
        public void ShouldReturnExpectedDeskBookingId(int? expectedDeskBookingId,bool isDeskAvailable) 
        {
            if (!isDeskAvailable)
            {
                _availableDesks.Clear();
            }else
            {
                _deskBookingMockRepository.Setup(x => x.SaveBooking(It.IsAny<DeskBooking>())).Callback<DeskBooking>(deskBooking => { deskBooking.Id = expectedDeskBookingId.Value; });
            }
            var response = _processor.BookDesk(_request);
            Assert.That(expectedDeskBookingId, Is.EqualTo(response.DeskBookingId));
        }
    }
}
