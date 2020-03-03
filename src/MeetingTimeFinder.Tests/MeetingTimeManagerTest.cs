using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MeetingTimeFinder.Tests
{
    public class MeetingTimeManagerTest
    {
        private MeetingTimeFinderManager MeetingTimeFinderManager { get; }

        public MeetingTimeManagerTest()
        {
            MeetingTimeFinderManager = new MeetingTimeFinderManager();
        }

        [Theory]
        [MemberData(nameof(GetOpenTimeFrameData))]
        public void GetOpenTimeFrames_Returns_Schedule_Time(
            string name,
            DateTime scheduleFrom,
            DateTime scheduleTo,
            IList<ITimeFrame> dailyEvents,
            IList<ITimeFrame> expectedOutput)
        {
            var personDailyCalendar = new PersonDailyCalendar(
                name, scheduleFrom, scheduleTo)
            {
                CalendarEvents = dailyEvents
            };

            var actual = new MeetingTimeFinderManager().GetOpenTimeFrames(personDailyCalendar).ToList();
            var expected = expectedOutput.ToList();

            Assert.Equal(name, personDailyCalendar.Name);
            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetOpenTimeFrameData()
        {
            const string timeFormat = "HH:mm";

            var allData = new List<object[]>
            {
                new object[]
                {
                    "Person X",
                    DateTime.ParseExact("07:00", timeFormat, null),
                    DateTime.ParseExact("16:00", timeFormat, null),
                    new List<ITimeFrame>(),
                    new List<ITimeFrame>
                    {
                        new TimeFrame("07:00", "16:00")
                    }
                },
                new object[]
                {
                    "Person Y",
                    DateTime.ParseExact("07:00", timeFormat, null),
                    DateTime.ParseExact("16:00", timeFormat, null),
                    new List<ITimeFrame>
                    {
                        new TimeFrame("07:00", "13:45"),
                        new TimeFrame("14:00", "16:00"),
                    },
                    new List<ITimeFrame>()
                },
                new object[]
                {
                    "Person Z",
                    DateTime.ParseExact("07:00", timeFormat, null),
                    DateTime.ParseExact("16:00", timeFormat, null),
                    new List<ITimeFrame>
                    {
                        new TimeFrame("07:00", "13:45"),
                        new TimeFrame("14:00", "15:30"),
                    },
                    new List<ITimeFrame>
                    {
                        new TimeFrame("15:30", "16:00"),
                    }
                },
                new object[]
                {
                    "Person A",
                    DateTime.ParseExact("09:00", timeFormat, null),
                    DateTime.ParseExact("20:00", timeFormat, null),
                    new List<ITimeFrame>
                    {
                        new TimeFrame("09:15", "10:30"),
                        new TimeFrame("12:00", "13:00"),
                        new TimeFrame("16:00", "18:00"),
                    },
                    new List<ITimeFrame>
                    {
                        new TimeFrame("10:30", "12:00"),
                        new TimeFrame("13:00", "16:00"),
                        new TimeFrame("18:00", "20:00"),
                    },
                },
                new object[]
                {
                    "Person B",
                    DateTime.ParseExact("10:00", timeFormat, null),
                    DateTime.ParseExact("18:30", timeFormat, null),
                    new List<ITimeFrame>
                    {
                        new TimeFrame("10:00", "11:30"),
                        new TimeFrame("12:30", "14:30"),
                        new TimeFrame("14:30", "15:00"),
                        new TimeFrame("16:00", "17:00"),
                    },
                    new List<ITimeFrame>
                    {
                        new TimeFrame("11:30", "12:30"),
                        new TimeFrame("15:00", "16:00"),
                        new TimeFrame("17:00", "18:30"),
                    },
                }
            };

            return allData;
        }
    }
}
