using System;
using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public class PersonDailyCalendar : IPerson, IDailyCalendar
    {
        public string Name { get; }
        public IList<ITimeFrame> CalendarEvents { get; set; } = new List<ITimeFrame>();
        public ITimeFrame Schedule { get; set; }

        public PersonDailyCalendar(string name, DateTime scheduleFrom, DateTime scheduleTo)
        {
            Name = name;
            CalendarEvents = new List<ITimeFrame>();
            Schedule = new TimeFrame(scheduleFrom, scheduleTo);
        }
    }
}
