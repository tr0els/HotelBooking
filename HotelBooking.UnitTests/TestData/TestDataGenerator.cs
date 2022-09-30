using HotelBooking.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.UnitTests.TestData
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetInvalidBookingStartDates()
        {
            var data = new List<object[]>
            {
                new object[] { DateTime.Today.AddDays(-1) },
                new object[] { DateTime.Today },
                new object[] { DateTime.Today.AddDays(1) },
            };

            return data;
        }

        public static IEnumerable<object[]> GetInvalidBookingPeriods()
        {
            var data = new List<object[]>
            {
                new object[] { DateTime.Today.AddDays(1), DateTime.Today.AddDays(10) },
                new object[] { DateTime.Today.AddDays(10), DateTime.Today.AddDays(15) },
                new object[] { DateTime.Today.AddDays(11), DateTime.Today.AddDays(25) },
            };
            return data;
        }

        public static IEnumerable<object[]> GetValidBookings()
        {
            var data = new List<object[]>
            {
                new object[] { new Booking { StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(9)} },
                new object[] { new Booking { StartDate = DateTime.Today.AddDays(5), EndDate = DateTime.Today.AddDays(6)} }
            };

            return data;
        }

        public static IEnumerable<object[]> GetPeriodsAndFullyOccupiedDates()
        {
            var data = new List<object[]>
            {
                // Three elements: Start date, end date and list of dates we expect to be fully occupied in between
                new object[] { DateTime.Today.AddDays(10), DateTime.Today.AddDays(20), GeneratePeriod(10, 20) },
                new object[] { DateTime.Today.AddDays(11), DateTime.Today.AddDays(19), GeneratePeriod(11, 19) },
                new object[] { DateTime.Today.AddDays(9), DateTime.Today.AddDays(11), GeneratePeriod(10, 11) },
                new object[] { DateTime.Today.AddDays(19), DateTime.Today.AddDays(21), GeneratePeriod(19, 20) },
                new object[] { DateTime.Today.AddDays(8), DateTime.Today.AddDays(9), new List<DateTime>() },
                new object[] { DateTime.Today.AddDays(21), DateTime.Today.AddDays(22), new List<DateTime>() }
            };

            return data;
        }

        private static List<DateTime> GeneratePeriod(int start, int end)
        {
            List<DateTime> period = new List<DateTime>();

            for (int i = start; i <= end; i++)
            {
                period.Add(DateTime.Today.AddDays(i));
            }

            return period;
        }
    }
}
