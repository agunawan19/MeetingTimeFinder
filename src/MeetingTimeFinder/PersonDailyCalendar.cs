using System;
using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public class PersonDailyCalendar : IPerson, IDailyCalendar
    {
        public string Name { get; }
        public IList<ITimeBlock> CalendarEvents { get; set; } = new List<ITimeBlock>();
        public ITimeBlock Schedule { get; set; }

        public PersonDailyCalendar(string name, DateTime scheduleFrom, DateTime scheduleTo)
        {
            Name = name;
            CalendarEvents = new List<ITimeBlock>();
            Schedule = new TimeBlock(scheduleFrom, scheduleTo);
        }
    }
}
