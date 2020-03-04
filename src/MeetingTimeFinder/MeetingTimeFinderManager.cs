using System.Collections.Generic;
using System.Linq;

namespace MeetingTimeFinder
{
    public class MeetingTimeFinderManager
    {
        public IList<PersonDailyCalendar> PersonDailyCalendars { get; set; } = new List<PersonDailyCalendar>();
        public int MeetingTimeToBeResolvedInMinutes { get; set; } = 30;

        public MeetingTimeFinderManager() { }

        public MeetingTimeFinderManager(IList<PersonDailyCalendar> personDailyCalendars) =>
            PersonDailyCalendars = personDailyCalendars;

        public IEnumerable<ITimeFrame> FindPossibleMeetingTime()
        {
            var possibleMeetingTimes = new List<ITimeFrame>();
            var openTimeFrames = PersonDailyCalendars
                .Select(personDailyCalendar => GetOpenTimeFrames(personDailyCalendar).ToList()).ToList();
            var openTimeFrames1 = openTimeFrames[0];
            var openTimeFrames2 = openTimeFrames[1];

            foreach (var openTimeFrame1 in openTimeFrames1)
            {
                foreach (var openTimeFrame2 in openTimeFrames2)
                {
                    if (openTimeFrame1.IntersecWith(openTimeFrame2))
                    {
                        var intersectedTimeFrame = GetIntersectedTimeFrame(openTimeFrame1, openTimeFrame2);

                        if (HasEnoughTime(intersectedTimeFrame))
                        {
                            possibleMeetingTimes.Add(intersectedTimeFrame);
                        }
                    }
                }
            }

            return possibleMeetingTimes;

            ITimeFrame GetIntersectedTimeFrame(ITimeFrame reference, ITimeFrame other) =>
                new TimeFrame
                {
                    From = reference.From.CompareTo(other.From) == -1 ? other.From : reference.From,
                    To = reference.To.CompareTo(other.To) == -1 ? reference.To : other.To
                };
        }

        public IEnumerable<ITimeFrame> GetOpenTimeFrames(PersonDailyCalendar personDailyCalendar)
        {
            var openTimeFrames = new List<ITimeFrame>();
            var dailySchedule = personDailyCalendar.Schedule;
            ITimeFrame currentEvent = null;
            ITimeFrame previousEvent = null;
            ITimeFrame openTimeFrame = null;

            foreach (var calendarEvents in personDailyCalendar.CalendarEvents)
            {
                currentEvent = calendarEvents;

                var isEventFromStartedAfterScheduleFrom =
                    currentEvent.From.CompareTo(dailySchedule.From) == 1;

                if (isEventFromStartedAfterScheduleFrom)
                {
                    var timeStartingPoint = previousEvent?.To ?? dailySchedule.From;
                    openTimeFrame = new TimeFrame
                    {
                        From = timeStartingPoint,
                        To = timeStartingPoint + currentEvent.From.Subtract(timeStartingPoint)
                    };
                    AddToList(openTimeFrames, openTimeFrame);
                }

                previousEvent = currentEvent;
            }

            openTimeFrame = new TimeFrame
            {
                From = currentEvent?.To ?? dailySchedule.From,
                To = dailySchedule.To
            };
            AddToList(openTimeFrames, openTimeFrame);

            return openTimeFrames;


            void AddToList(List<ITimeFrame> openTimeList, ITimeFrame openTimeItem)
            {
                if (HasEnoughTime(openTimeItem))
                {
                    openTimeList.Add(openTimeItem);
                }
            }
        }

        private bool HasEnoughTime(ITimeFrame openTimeFrame) =>
            openTimeFrame.From.AddMinutes(MeetingTimeToBeResolvedInMinutes) <= openTimeFrame.To;
    }
}
