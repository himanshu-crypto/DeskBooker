using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;
using System;
using Xunit;

namespace DeskBooker.Core.Processor
{
    public class DeskBookerRequestProcessorTests
    {
        private readonly DeskBookingRequest _request;
        private readonly Mock<IDeskBookingRepository> _deskBookingMockRepository;
        private readonly DeskBookingRequestProcessor _processor;

        public DeskBookerRequestProcessorTests()
        {
             _request = new DeskBookingRequest
            {
                FirstName = "Himanshu",
                LastName = "Chauhan",
                Email = "himanshuc.dev@gmail.com",
                Date = new DateTime(2020, 1, 28)
            };

            _deskBookingMockRepository = new Mock<IDeskBookingRepository>();
            _processor = new DeskBookingRequestProcessor(_deskBookingMockRepository.Object);
        }
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            //Arrange
           

            //Act
            DeskBookingResult result = _processor.BookDesk(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
            Assert.Equal(_request.LastName, result.LastName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);
        }
        [Fact]
        public void ShouldThrowExceptionIFRequestIsNull()
        {

            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));

            Assert.Equal("request", exception.ParamName);
        }
        [Fact]
        public void ShouldSaveDeskBooking()
        {
            DeskBooking savedDeskBooking= null;
            _deskBookingMockRepository.Setup(x => x.Save(It.IsAny<DeskBooking>())).Callback<DeskBooking>(deskBooking =>
            {
                savedDeskBooking = deskBooking;
            });
            _processor.BookDesk(_request);

            _deskBookingMockRepository.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);

            Assert.NotNull(savedDeskBooking);
            Assert.Equal(savedDeskBooking.FirstName, _request.FirstName);
        }
    }
}
