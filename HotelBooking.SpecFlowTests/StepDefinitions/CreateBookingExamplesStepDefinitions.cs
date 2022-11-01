using HotelBooking.Core;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Repositories;
using Moq;
using System;
using TechTalk.SpecFlow;

namespace HotelBooking.SpecFlowTests.StepDefinitions
{
    [Binding]
    public class CreateBookingExamplesStepDefinitions
    {
        List<Booking> bookings;
        BookingManager manager;
        DateTime startDate;
        DateTime endDate;
        bool result;

        [BeforeScenario]
        public void MockSetup()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=start, EndDate=end, IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=start, EndDate=end, IsActive=true, CustomerId=2, RoomId=2 },
            };

            var fakeBookingRepository = new Mock<IRepository<Booking>>();
            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
            fakeBookingRepository.Setup(x => x.Add(It.IsAny<Booking>())).Verifiable();
            List<Room> rooms = new List<Room>
            {
                new Room { Id=1, Description="A" },
                new Room { Id=2, Description="B" },
            };

            var fakeRoomRepository = new Mock<IRepository<Room>>();
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);

            manager = new BookingManager(fakeBookingRepository.Object, fakeRoomRepository.Object);
        }   

        [Given(@"that the start date is (.*) days from now")]
        public void GivenThatTheStartDateIsDaysFromNow(int startOffset)
        {
            startDate = DateTime.Today.AddDays(startOffset);
        }

        [Given(@"and the end date is (.*) days from now")]
        public void GivenAndTheEndDateIsDaysFromNow(int endOffset)
        {
            endDate = DateTime.Today.AddDays(endOffset);
        }

        [When(@"the method is called")]
        public void WhenTheMethodIsCalled()
        {
            Booking booking = new Booking { StartDate = startDate, EndDate = endDate };
            result = manager.CreateBooking(booking);
        }

        [Then(@"the result should be '(.*)'")]
        public void ThenTheResultShouldBe(bool validBooking)
        {
            Assert.Equal(validBooking, result);
        }
    }
}
