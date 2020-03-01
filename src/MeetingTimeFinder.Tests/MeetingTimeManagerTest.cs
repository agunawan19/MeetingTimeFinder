using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MeetingTimeFinder;
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
        [MemberData(nameof(GetOpenTimeBlockData), parameters: new object[] { 0, 3 })]
        public void GetOpenTimeBlocks_Returns_Schedule_Time(
            string name,
            DateTime scheduleFrom,
            DateTime scheduleTo,
            IList<ITimeBlock> dailyEvents,
            IList<ITimeBlock> expectedOutput)
        {
            var personDailyCalendar = new PersonDailyCalendar(
                name, scheduleFrom, scheduleTo)
            {
                CalendarEvents = dailyEvents
            };

            var actual = new MeetingTimeFinderManager().GetOpenTimeBlocks(personDailyCalendar).ToList();
            var expected = expectedOutput.ToList();

            Assert.Equal(name, personDailyCalendar.Name);
            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetOpenTimeBlockData(
            int numSkips, int numTests)
        {
            const string timeFormat = "HH:mm";

            var allData = new List<object[]>
            {
                new object[]
                {
                    "Person X",
                    DateTime.ParseExact("07:00", timeFormat, null),
                    DateTime.ParseExact("16:00", timeFormat, null),
                    new List<ITimeBlock>(),
                    new List<ITimeBlock>
                    {
                        new TimeBlock("07:00", "16:00")
                    }
                },
                new object[]
                {
                    "Person A",
                    DateTime.ParseExact("09:00", timeFormat, null),
                    DateTime.ParseExact("20:00", timeFormat, null),
                    new List<ITimeBlock>
                    {
                        new TimeBlock("09:15", "10:30"),
                        new TimeBlock("12:00", "13:00"),
                        new TimeBlock("16:00", "18:00"),
                    },
                    new List<ITimeBlock>
                    {
                        new TimeBlock("10:30", "12:00"),
                        new TimeBlock("13:00", "16:00"),
                        new TimeBlock("18:00", "20:00"),
                    },
                },
                new object[]
                {
                    "Person B",
                    DateTime.ParseExact("10:00", timeFormat, null),
                    DateTime.ParseExact("18:30", timeFormat, null),
                    new List<ITimeBlock>
                    {
                        new TimeBlock("10:00", "11:30"),
                        new TimeBlock("12:30", "14:30"),
                        new TimeBlock("14:30", "15:00"),
                        new TimeBlock("16:00", "17:00"),
                    },
                    new List<ITimeBlock>
                    {
                        new TimeBlock("11:30", "12:30"),
                        new TimeBlock("15:00", "16:00"),
                        new TimeBlock("17:00", "18:30"),
                    },
                }
            };

            return allData.Skip(numSkips).Take(numTests);
        }
    }
}
