using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public class DailyCalendar : IDailyCalendar
    {
        public IList<ITimeFrame> CalendarEvents { get; set; } = new List<ITimeFrame>();
    }
}
