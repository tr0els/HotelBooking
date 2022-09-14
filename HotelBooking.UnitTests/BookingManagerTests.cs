using System;
using System.Collections.Generic;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        public static IEnumerable<object[]> GetInvalidStartDates()
        {
            var data = new List<object[]>
            {
                new object[] { DateTime.Today.AddDays(-1) },
                new object[] { DateTime.Today },
                new object[] { DateTime.Today.AddDays(1) },
            };

            return data;
        }

        [Theory]
        [MemberData(nameof(GetInvalidStartDates))]
        public void FindAvailableRoom_StartDateNotInFutureOrBeforeEndDate_ThrowsArgumentException(DateTime startDate)
        {
            // Arrange
            DateTime endDate = DateTime.Today;


            // Act
            Action act = () => bookingManager.FindAvailableRoom(startDate, endDate);

            // Assert
            Assert.Throws<ArgumentException>(act);
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
        }

    }
}
