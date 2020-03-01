using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public interface IDailyCalendar
    {
        IList<ITimeBlock> CalendarEvents { get; set; }
    }
}
