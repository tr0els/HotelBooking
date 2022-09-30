using System;
using System.Collections.Generic;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using HotelBooking.UnitTests.TestData;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;
        private Mock<IRepository<Booking>> fakeBookingRepository;
        private Mock<IRepository<Room>> fakeRoomRepository;
        DateTime start;
        DateTime end;

        public BookingManagerTests(){
            #region fakeBookingRepository Setup
            start = DateTime.Today.AddDays(10);
            end = DateTime.Today.AddDays(20);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=start, EndDate=end, IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=start, EndDate=end, IsActive=true, CustomerId=2, RoomId=2 },
            };

            fakeBookingRepository = new Mock<IRepository<Booking>>();
            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
            #endregion

            #region fakeRoomRepository Setup
            List<Room> rooms = new List<Room>
            {
                new Room { Id=1, Description="A" },
                new Room { Id=2, Description="B" },
            };
            
            fakeRoomRepository = new Mock<IRepository<Room>>();            
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);
            #endregion

            bookingManager = new BookingManager(fakeBookingRepository.Object, fakeRoomRepository.Object);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetInvalidBookingStartDates),
            MemberType = typeof(TestDataGenerator))]
        public void FindAvailableRoom_StartDateNotInFutureOrBeforeEndDate_ThrowsArgumentException(DateTime startDate)
        {
            // Arrange
            DateTime endDate = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert            
            Assert.Throws<ArgumentException>(act);
            fakeBookingRepository.Verify(x => x.GetAll(), Times.Never);
            fakeRoomRepository.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
            fakeBookingRepository.Verify(x => x.GetAll(), Times.Once);
            fakeRoomRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetInvalidBookingPeriods),
            MemberType = typeof(TestDataGenerator))]
        public void FindAvailableRoom_RoomNotAvailable_RoomIdMinusOne(DateTime startDate, DateTime endDate)
        {
            // Arrange
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, endDate);
            // Assert
            Assert.Equal(-1, roomId);
            fakeBookingRepository.Verify(x => x.GetAll(), Times.Once);
            fakeRoomRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void GetFullyOccupiedDates_StartLaterThanEndDate_ThrowsArgumentException()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today;

            // Act
            Action act = () => bookingManager.GetFullyOccupiedDates(startDate, endDate);

            // Assert            
            Assert.Throws<ArgumentException>(act);
            fakeBookingRepository.Verify(x => x.GetAll(), Times.Never);
            fakeRoomRepository.Verify(x => x.GetAll(), Times.Never);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetPeriodsAndFullyOccupiedDates), 
            MemberType = typeof(TestDataGenerator))]
        public void GetFullyOccupiedDates_CheckPeriod_ReturnCorrectDates(DateTime startDate, DateTime endDate, List<DateTime> correctDates)
        {
            // Act
            var actualOccupiedDates = bookingManager.GetFullyOccupiedDates(startDate, endDate);

            // Assert
            Assert.Equal(correctDates, actualOccupiedDates);
            fakeBookingRepository.Verify(x => x.GetAll(), Times.Once);
            fakeRoomRepository.Verify(x => x.GetAll(), Times.Once);
        }

    }
}
