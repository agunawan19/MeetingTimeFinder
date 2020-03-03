using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public interface IDailyCalendar
    {
        IList<ITimeFrame> CalendarEvents { get; set; }
    }
}
