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
    }
}
