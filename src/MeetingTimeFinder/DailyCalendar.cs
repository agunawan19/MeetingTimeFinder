using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public class DailyCalendar : IDailyCalendar
    {
        public IList<ITimeBlock> CalendarEvents { get; set; } = new List<ITimeBlock>();
    }
}
